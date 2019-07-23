import { UserManager, WebStorageStateStore, Oidc } from 'oidc-client';
import Vue from 'vue';

const authConfig = {
  authority: 'http://localhost:5000',
  client_id: 'frontStore',
  redirect_uri: 'http://localhost:8080/callback',
  response_type: 'code',
  scope: 'openid profile api1 IdentityServerApi',
  post_logout_redirect_uri: 'http://localhost:8080',
  userStore: new WebStorageStateStore({ store: window.localStorage }),

  clockSkew: 15,
  accessTokenExpiringNotificationTime: 40,

  automaticSilentRenew: true,
  silent_redirect_uri: 'http://localhost:8080/silentrenew',
  filterProtocolClaims: true,
};

const userManager = new UserManager(authConfig);

const auth = new Vue({
  data() {
    return {
      accessToken: null,
      expiresAt: null,
    };
  },
  computed: {
    isAuthenticated() {
      console.log(`auth init${this.accessToken && true}`);
      return this.accessToken && new Date().getTime() < this.expiresAt;
    },
  },
  created() {
    userManager.getUser().then((user) => {
      if (user) {
        this.accessToken = user.access_token;
        this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
      } else {
        this.accessToken = null;
        this.expiresAt = null;
      }
    });

    userManager.events.addAccessTokenExpiring(() => {
      console.log('Token is about to expire...');
    });

    userManager.events.addUserLoaded((user) => {
      console.log('User loaded');
      this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
      this.accessToken = user.access_token;
    });

    userManager.events.addAccessTokenExpired(() => {
      console.log('Token is expired. Trying to renew');
      userManager.signinSilent();
    });
  },
  methods: {
    login() {
      userManager.signinRedirect();
    },

    getUser() {
      return userManager.getUser();
    },

    logout() {
      userManager.signoutRedirect();
      this.accessToken = null;
      this.expiresAt = null;
    },

    authCallback() {
      userManager.signinRedirectCallback().then((user) => {
        window.location.href = '../';
        this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
        this.accessToken = user.access_token;
      }).catch((err) => {
        console.log(err);
      });
    },

    silentCallback() {
      console.log('Silent renew callback');
      userManager.signinSilentCallback();
    },
  },
});

export default {
  install(vue) {
    vue.prototype.$auth = auth;
  },
};
