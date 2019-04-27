import { UserManager, WebStorageStateStore } from 'oidc-client';

const authConfig = {
  authority: 'http://localhost:5000',
  client_id: 'frontStore',
  redirect_uri: 'http://localhost:8080/callback',
  response_type: 'code',
  scope: 'openid profile api1',
  post_logout_redirect_uri: 'http://localhost:8080',
  userStore: new WebStorageStateStore({ store: window.localStorage }),

  // automaticSilentRenew: true,
  // silent_redirect_uri: 'https://localhost:44357/silent-renew.html',
  // filterProtocolClaims: true,
};

const auth = {
  function() {
    this.isAuthenticated = true;
    // this.mgr.sig
  },

  mgr: new UserManager(authConfig),
  isAuthenticated: false,

  login() {
    this.mgr.signinRedirect();
  },

  getUser() {
    return this.mgr.getUser();
  },

  logout() {
    this.mgr.signoutRedirect();
  },

  getAccessToken() {
    return this.mgr.getUser().then((data) => {
      console.log(data);
      return data.access_token;
    });
  },

  authCallback() {
    const mgr = new UserManager({ response_mode: 'query', userStore: new WebStorageStateStore() });

    mgr.signinRedirectCallback().then((user) => {
      window.location.href = '../';
    }).catch((err) => {
      console.log(err);
    });
  },
};

export default {
  install(Vue) {
    Vue.prototype.$auth = auth;
  },
};
