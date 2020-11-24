import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);

//要设置的全局访问的state对象
const state = {
    //要设置的初始属性值
    loginInfo: {
        user: null,
        token: ""
    }
};

const getters = {   //实时监听state值的变化(最新状态)
    getLoginInfoGetters(state) {
        return state.loginInfo;
    }
};

const mutations = {
    setLoginInfoMutations(state, stateValue) {
        var loginInfoJsonStr = JSON.stringify(stateValue);
        localStorage.setItem("loginInfo", loginInfoJsonStr);
    },
    getLoginInfoMutations(state) {
        var loginInfoJsonObj = JSON.parse(localStorage.getItem("loginInfo"));
        state.loginInfo = loginInfoJsonObj;
    }
};

const actions = {
    setLoginInfoActions(context, stateValue) {
        context.commit('setLoginInfoMutations', stateValue);
    }
};

const store = new Vuex.Store({
    state,
    getters,
    mutations,
    actions
});
export default store;