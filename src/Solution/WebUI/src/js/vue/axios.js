import router from '@/js/vue/router';

//vue使用axios做ajax请求
import axios from 'axios';
//让ajax情况携带cookie，不设置后端获取不到session值
axios.defaults.withCredentials = true;

// http request 请求拦截器
axios.interceptors.request.use(
    config => {
        let pathname = location.pathname;
        if (pathname !== '/' && pathname !== '/Login') {
            var token = JSON.parse(localStorage.getItem("loginInfo")).token;
            if (token) {
                // Bearer是JWT的认证头部信息
                config.headers.common['Authorization'] = 'Bearer ' + token;
            }
        }
        return config;
    },
    error => {
        return Promise.reject(error);
    }
);

// http response 响应拦截器
axios.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        if (error.response) {
            switch (error.response.status) {
                // 返回401，清除token信息并跳转到登录页面
                case 401:
                    alert('您的登录信息已失效，请重新登录！');
                    localStorage.removeItem('loginInfo');
                    router.push('/');
                    break;
                default:
                    alert('服务器存在异常!');
                    break;
            }
            // 返回接口返回的错误信息
            return Promise.reject(error.response.data);
        }
    }
);

export default axios;
