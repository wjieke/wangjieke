说明：
部署到Internet Information Services (IIS)Vue路由HTML5 History模式 
需要在网站根目录中创建一个 web.config 文件
详细访问：
https://router.vuejs.org/zh/guide/essentials/history-mode.html#%E5%90%8E%E7%AB%AF%E9%85%8D%E7%BD%AE%E4%BE%8B%E5%AD%90

Vue模板只能有一个根对象，不能有平级节点，否则报如下错
Component template should contain exactly one root element. If you are using v-if on multiple elemen

安装项目依赖：npm install（SVN拉取项目下来先初始化安装项目依赖包）

---------------------------------------------------------------------------------------------------------------------------
安装sass：（目前项目中，因为下载sass有坑，所以卸载了，没用到。）
npm install --global --production windows-build-tools
npm install node-sass --save-dev
npm install sass-loader --save-dev
---------------------------------------------------------------------------------------------------------------------------