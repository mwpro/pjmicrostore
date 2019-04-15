import { UserManager, WebStorageStateStore } from "oidc-client";
import Vue from 'vue';

const authConfig = {
    authority: "http://localhost:5000",
    client_id: "frontStore",
    redirect_uri: "http://localhost:8080/callback",
    response_type: "code",
    scope: "openid profile api1",
    post_logout_redirect_uri: "http://localhost:8080",
    userStore: new WebStorageStateStore({ store: window.localStorage }),
};

const auth = {
    mgr: new UserManager(authConfig),

    login: function () {
        this.mgr.signinRedirect();
    },

    api: function () {
        this.mgr.getUser().then(function (user) {
            var url = "http://localhost:5001/identity";

            var xhr = new XMLHttpRequest();
            xhr.open("GET", url);
            xhr.onload = function () {
                log(xhr.status, JSON.parse(xhr.responseText));
            }
            xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr.send();
        });
    },

    logout: function () {
        this.mgr.signoutRedirect();
    },

    authCallback: function () {
        var mgr = new UserManager({ response_mode: "query", userStore: new WebStorageStateStore() });

        mgr.signinRedirectCallback().then(function (user) {
            window.location.href = '../';
        }).catch(function (err) {
            console.log(err);
        });
    }
}

export default {
    install(Vue) {
        Vue.prototype.$auth = auth;
    },
};