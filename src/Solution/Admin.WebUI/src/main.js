import Vue from 'vue';
import App from './App.vue';
import router from './js/vue/router';
import store from './js/vue/store';
import axios from './js/vue/axios';

//注册axios为Vue的原型属性
Vue.prototype.$axios = axios;

//ElementUI组件库
import ElementUI from './js/element/element-ui';

//vue使用第三方模块使用use载入使用模块
Vue.use(ElementUI);

//设置为 false 以阻止 vue 在启动时生成生产提示。
Vue.config.productionTip = true;

new Vue({
    router,
    store,
    render: h => h(App)
}).$mount('#app');
