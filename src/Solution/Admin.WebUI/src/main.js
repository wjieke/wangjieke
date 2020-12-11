import Vue from 'vue';
import App from './App.vue';
import router from './js/vue/router';
import store from './js/vue/store';
import axios from './js/vue/axios';
import Fragment from 'vue-fragment';
//ElementUI组件库
import ElementUI from './js/element/element-ui';

//注册axios为Vue的原型属性（功能：全局使用axios）
Vue.prototype.$axios = axios;
//注册eventBus为Vue的原型属性（功能：全局使用eventBus）
Vue.prototype.$eventBus = new Vue();

//设置为 false 以阻止 vue 在启动时生成生产提示。
Vue.config.productionTip = true;

//使用element-ui，vue使用第三方模块使用use载入使用模块
Vue.use(ElementUI);
//使用vue-fragment（功能：可以组件模板有多个并级节点）
Vue.use(Fragment.Plugin);

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
