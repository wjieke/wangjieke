let baseUrl = '';
switch (process.env.NODE_ENV) {
    case 'development':
        //开发环境API
        baseUrl = 'https://localhost:44372/api'
        break
    case 'production':
        //生产环境API
        baseUrl = 'http://www.wangjk.wang:8090/api'
        break
}

//通用枚举
export const enumUrl = {
    getSexType: baseUrl + '/Utility/GetSexType',
    getDataState: baseUrl + '/Utility/GetDataState',
    getClientCategory: baseUrl + '/Utility/GetClientCategory'
}
//登录模块
export const loginUrl = {
    verificationCode: baseUrl + '/VerifyCode/MixVerifyCode',
    verificationLogin: baseUrl + '/User/UserLogin'
}
//用户管理
export const userUrl = {
    addInfo: baseUrl + '/User/AddInfo',
    modInfo: baseUrl + '/User/ModInfo',
    delInfo: baseUrl + '/User/DelInfo',
    getInfo: baseUrl + '/User/GetInfo',
    getPage: baseUrl + '/User/GetPage',
    getList: baseUrl + '/User/GetList'
}
//角色管理
export const roleUrl = {
    addInfo: baseUrl + '/Role/AddInfo',
    modInfo: baseUrl + '/Role/ModInfo',
    delInfo: baseUrl + '/Role/DelInfo',
    getInfo: baseUrl + '/Role/GetInfo',
    getPage: baseUrl + '/Role/GetPage',
    getNode: baseUrl + '/Role/GetNode',
    getTree: baseUrl + '/Role/GetTree',
    getPath: baseUrl + '/Role/GetPath',
    getList: baseUrl + '/Role/GetList'
}
//权限管理
export const permissionUrl = {
    addInfo: baseUrl + '/Permission/AddInfo',
    modInfo: baseUrl + '/Permission/ModInfo',
    delInfo: baseUrl + '/Permission/DelInfo',
    getInfo: baseUrl + '/Permission/GetInfo',
    getPage: baseUrl + '/Permission/GetPage',
    getNode: baseUrl + '/Permission/GetNode',
    getTree: baseUrl + '/Permission/GetTree',
    getPath: baseUrl + '/Permission/GetPath',
    getList: baseUrl + '/Permission/GetList'
}
//公司管理
export const companyUrl = {
    addInfo: baseUrl + '/Company/AddInfo',
    modInfo: baseUrl + '/Company/ModInfo',
    delInfo: baseUrl + '/Company/DelInfo',
    getInfo: baseUrl + '/Company/GetInfo',
    getPage: baseUrl + '/Company/GetPage',
    getList: baseUrl + '/Company/GetList'
}
//部门管理
export const departmentUrl = {
    addInfo: baseUrl + '/Department/AddInfo',
    modInfo: baseUrl + '/Department/ModInfo',
    delInfo: baseUrl + '/Department/DelInfo',
    getInfo: baseUrl + '/Department/GetInfo',
    getPage: baseUrl + '/Department/GetPage',
    getNode: baseUrl + '/Department/GetNode',
    getTree: baseUrl + '/Department/GetTree',
    getPath: baseUrl + '/Department/GetPath',
    getList: baseUrl + '/Department/GetList'
}
//地区管理
export const areaUrl = {
    addInfo: baseUrl + '/Area/AddInfo',
    modInfo: baseUrl + '/Area/ModInfo',
    delInfo: baseUrl + '/Area/DelInfo',
    getInfo: baseUrl + '/Area/GetInfo',
    getPage: baseUrl + '/Area/GetPage',
    getNode: baseUrl + '/Area/GetNode',
    getTree: baseUrl + '/Area/GetTree',
    getPath: baseUrl + '/Area/GetPath',
    getList: baseUrl + '/Area/GetList'
}
//菜单管理
export const menuUrl = {
    addInfo: baseUrl + '/Menu/AddInfo',
    modInfo: baseUrl + '/Menu/ModInfo',
    delInfo: baseUrl + '/Menu/DelInfo',
    getInfo: baseUrl + '/Menu/GetInfo',
    getPage: baseUrl + '/Menu/GetPage',
    getNode: baseUrl + '/Menu/GetNode',
    getTree: baseUrl + '/Menu/GetTree',
    getPath: baseUrl + '/Menu/GetPath',
    getList: baseUrl + '/Menu/GetList'
}

//博客文章管理
export const articleUrl = {
    addInfo: baseUrl + '/Article/AddInfo',
    modInfo: baseUrl + '/Article/ModInfo',
    delInfo: baseUrl + '/Article/DelInfo',
    getInfo: baseUrl + '/Article/GetInfo',
    getPage: baseUrl + '/Article/GetPage'
}