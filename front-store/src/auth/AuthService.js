import { UserManager, WebStorageStateStore, Oidc } from 'oidc-client';
import Vue from 'vue';

const authConfig = {
  authority: 'http://localhost:5000',
  client_id: 'frontStore',
  redirect_uri: 'http://localhost:8080/callback',
  response_type: 'code',
  scope: 'openid profile api1',
  post_logout_redirect_uri: 'http://localhost:8080',
  userStore: new WebStorageStateStore({ store: window.localStorage }),

  clockSkew: 15,
  accessTokenExpiringNotificationTime: 40,

  automaticSilentRenew: true,
  silent_redirect_uri: 'http://localhost:8080/silentrenew',
  filterProtocolClaims: true,
};

const auth = new Vue({
  data() {
    return {
      accessToken: null,
      expiresAt: null,
      userManager: new UserManager(authConfig),
    };
  },
  computed: {
    isAuthenticated() {
      return this.accessToken && new Date().getTime() < this.expiresAt;
    },
  },
  created() {
    this.userManager.getUser().then((user) => {
      this.accessToken = user.access_token;
      this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
    });

    this.userManager.events.addAccessTokenExpiring(() => {
      console.log('Token is about to expire...');
    });

    this.userManager.events.addUserLoaded((user) => {
      console.log('User loaded');
      console.log(user);
      this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
      this.accessToken = user.access_token;
    });

    this.userManager.events.addAccessTokenExpired(() => {
      console.log('Token is expired. Trying to renew');
      this.userManager.signinSilent();
    });
  },
  methods: {
    login() {
      this.userManager.signinRedirect();
    },

    getUser() {
      return this.userManager.getUser();
    },

    logout() {
      this.userManager.signoutRedirect();
      this.accessToken = null;
      this.expiresAt = null;
    },

    authCallback() {
      this.userManager.signinRedirectCallback().then((user) => {
        window.location.href = '../';
        this.expiresAt = user.expires_at ? user.expires_at * 1000 : null;
        this.accessToken = user.access_token;
      }).catch((err) => {
        console.log(err);
      });
    },

    silentCallback() {
      console.log('Silent renew callback');
      this.userManager.signinSilentCallback();
    },
  },
});

export default {
  install(vue) {
    vue.prototype.$auth = auth;
  },
};
