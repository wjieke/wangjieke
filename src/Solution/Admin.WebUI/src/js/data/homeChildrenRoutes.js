export const homeChildrenRoutes = [
    {
        path: '/Index',
        name: 'Index',
        meta: { title: '系统首页' },
        component: resolve => require(['@/components/public/Index.vue'], resolve)
    }, 
    //#region ======================================平台系统管理=================================================
    {
        path: '/User',
        name: 'User',
        meta: { title: '用户管理' },
        component: resolve => require(['@/components/system/User.vue'], resolve)
    },
    {
        path: '/Company',
        name: 'Company',
        meta: { title: '公司管理' },
        component: resolve => require(['@/components/system/Company.vue'], resolve)
    },
    {
        path: '/Department',
        name: 'Department',
        meta: { title: '部门管理' },
        component: resolve => require(['@/components/system/Department.vue'], resolve)
    },
    {
        path: '/Area',
        name: 'Area',
        meta: { title: '地区管理' },
        component: resolve => require(['@/components/system/Area.vue'], resolve)
    },
    {
        path: '/Menu',
        name: 'Menu',
        meta: { title: '菜单管理' },
        component: resolve => require(['@/components/system/Menu.vue'], resolve)
    },
    {
        path: '/Permission',
        name: 'Permission',
        meta: { title: '权限管理' },
        component: resolve => require(['@/components/system/Permission.vue'], resolve)
    },
    {
        path: '/Role',
        name: 'Role',
        meta: { title: '角色管理' },
        component: resolve => require(['@/components/system/Role.vue'], resolve)
    },
    //#endregion ================================================================================================
    //#region ======================================基础信息管理=================================================
    //{
    //    path: '/Residential',
    //    name: 'Residential',
    //    meta: { title: '小区管理' },
    //    component: resolve => require(['@/components/news/NewsType.vue'], resolve)
    //},
    //#endregion ================================================================================================
    //#region ======================================博客网站管理=================================================
    {
        path: '/Article',
        name: 'Article',
        meta: { title: '博客文章' },
        component: resolve => require(['@/components/blog/Article.vue'], resolve)
    },
    //#endregion ================================================================================================
    //#region ======================================官网网站管理=================================================
    {
        path: '/NewsType',
        name: 'NewsType',
        meta: { title: '新闻类型' },
        component: resolve => require(['@/components/site/NewsType.vue'], resolve)
    },
    {
        path: '/News',
        name: 'News',
        meta: { title: '新闻管理' },
        component: resolve => require(['@/components/site/News.vue'], resolve)
    }, 
    //#endregion ================================================================================================
    //#region ======================================其他模块管理=================================================
    {
        path: '/component01',
        name: 'component01',
        meta: { title: 'Page01' },
        component: resolve => require(['@/components/other/component01.vue'], resolve)
    },
    {
        path: '/component02',
        name: 'component02',
        meta: { title: 'Page02' },
        component: resolve => require(['@/components/other/component02.vue'], resolve)
    },
    {
        path: '/component03',
        name: 'component03',
        meta: { title: 'Page03' },
        component: resolve => require(['@/components/other/component03.vue'], resolve)
    },
    {
        path: '/component04',
        name: 'component04',
        meta: { title: 'Page04' },
        component: resolve => require(['@/components/other/component04.vue'], resolve)
    },
    {
        path: '/component05',
        name: 'component05',
        meta: { title: 'Page05' },
        component: resolve => require(['@/components/other/component05.vue'], resolve)
    },
    {
        path: '/component06',
        name: 'component06',
        meta: { title: 'Page06' },
        component: resolve => require(['@/components/other/component06.vue'], resolve)
    },
    {
        path: '/component07',
        name: 'component07',
        meta: { title: 'Page07' },
        component: resolve => require(['@/components/other/component07.vue'], resolve)
    },
    {
        path: '/component08',
        name: 'component08',
        meta: { title: 'Page08' },
        component: resolve => require(['@/components/other/component08.vue'], resolve)
    },
    {
        path: '/component09',
        name: 'component09',
        meta: { title: 'Page09' },
        component: resolve => require(['@/components/other/component09.vue'], resolve)
    },
    {
        path: '/component10',
        name: 'component10',
        meta: { title: 'Page10' },
        component: resolve => require(['@/components/other/component10.vue'], resolve)
    },
    {
        path: '/component11',
        name: 'component11',
        meta: { title: 'Page11' },
        component: resolve => require(['@/components/other/component11.vue'], resolve)
    },
    {
        path: '/component12',
        name: 'component12',
        meta: { title: 'Page12' },
        component: resolve => require(['@/components/other/component12.vue'], resolve)
    },
    {
        path: '/component13',
        name: 'component13',
        meta: { title: 'Page13' },
        component: resolve => require(['@/components/other/component13.vue'], resolve)
    },
    {
        path: '/component14',
        name: 'component14',
        meta: { title: 'Page14' },
        component: resolve => require(['@/components/other/component14.vue'], resolve)
    },
    {
        path: '/component15',
        name: 'component15',
        meta: { title: 'Page15' },
        component: resolve => require(['@/components/other/component15.vue'], resolve)
    },
    {
        path: '/component16',
        name: 'component16',
        meta: { title: 'Page16' },
        component: resolve => require(['@/components/other/component16.vue'], resolve)
    },
    {
        path: '/component17',
        name: 'component17',
        meta: { title: 'Page17' },
        component: resolve => require(['@/components/other/component17.vue'], resolve)
    },
    {
        path: '/component18',
        name: 'component18',
        meta: { title: 'Page18' },
        component: resolve => require(['@/components/other/component18.vue'], resolve)
    },
    {
        path: '/component19',
        name: 'component19',
        meta: { title: 'Page19' },
        component: resolve => require(['@/components/other/component19.vue'], resolve)
    },
    {
        path: '/component20',
        name: 'component20',
        meta: { title: 'Page20' },
        component: resolve => require(['@/components/other/component20.vue'], resolve)
    }
    //#endregion ================================================================================================

]