import Vue from 'vue';
import VueRouter from 'vue-router';
Vue.use(VueRouter);
//解决已存在的路由，再次push出现（Navigating to current location ("/User") is not allowed）不允许导航到当前位置的错误
const VueRouterPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(to) {
    return VueRouterPush.call(this, to).catch(err => err)
}
//import引入时用到的“@”代表根节点
//vue路由require写法是路由按需加载(懒加载)，访问此路由时才加载这个js组件
import Home from '@/components/layout/Home.vue';
import Login from '../../components/layout/Login.vue';
import { homeChildrenRoutes } from '@/js/data/homeChildrenRoutes.js';

var routes = [
    { path: '/', redirect: '/Login' },
    { path: '*', redirect: '/404' },
    {
        path: '/Login',
        name: 'Login',
        meta: { title: '系统登录' },
        component: Login
    },
    {
        path: '/404',
        name: '404',
        meta: { title: '错误页面' },
        component: resolve => require(['@/components/layout/404.vue'], resolve)
    },
    {
        path: '/Home',
        name: 'Home',
        meta: { title: '系统主页' },
        component: Home,
        redirect: '/Index',
        children: homeChildrenRoutes
    }
];

var router = new VueRouter({
    mode: 'history',//去掉路由#号
    routes
});
export default router;