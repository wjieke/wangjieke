export const homeChildrenRoutes = [
    {
        path: '/Index',
        name: 'Index',
        meta: { title: '系统首页' },
        component: resolve => require(['@/components/layout/Index.vue'], resolve)
    },
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
        path: '/NewsType',
        name: 'NewsType',
        meta: { title: '新闻类型' },
        component: resolve => require(['@/components/news/NewsType.vue'], resolve)
    },
    {
        path: '/NewsManage',
        name: 'NewsManage',
        meta: { title: '新闻管理' },
        component: resolve => require(['@/components/news/NewsManage.vue'], resolve)
    },
    //#endregion ================================================================================================
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
    //#region ======================================官网网站管理=================================================
    {
        path: '/component1',
        name: 'component1',
        meta: { title: 'Page1' },
        component: resolve => require(['@/components/website/component1.vue'], resolve)
    },
    {
        path: '/component2',
        name: 'component2',
        meta: { title: 'Page2' },
        component: resolve => require(['@/components/website/component2.vue'], resolve)
    },
    {
        path: '/component3',
        name: 'component3',
        meta: { title: 'Page3' },
        component: resolve => require(['@/components/website/component3.vue'], resolve)
    },
    {
        path: '/component4',
        name: 'component4',
        meta: { title: 'Page4' },
        component: resolve => require(['@/components/website/component4.vue'], resolve)
    },
    {
        path: '/component5',
        name: 'component5',
        meta: { title: 'Page5' },
        component: resolve => require(['@/components/website/component5.vue'], resolve)
    },
    {
        path: '/component6',
        name: 'component6',
        meta: { title: 'Page6' },
        component: resolve => require(['@/components/website/component6.vue'], resolve)
    },
    {
        path: '/component7',
        name: 'component7',
        meta: { title: 'Page7' },
        component: resolve => require(['@/components/website/component7.vue'], resolve)
    },
    {
        path: '/component8',
        name: 'component8',
        meta: { title: 'Page8' },
        component: resolve => require(['@/components/website/component8.vue'], resolve)
    },
    {
        path: '/component9',
        name: 'component9',
        meta: { title: 'Page9' },
        component: resolve => require(['@/components/website/component9.vue'], resolve)
    },
    {
        path: '/component10',
        name: 'component10',
        meta: { title: 'Page10' },
        component: resolve => require(['@/components/website/component10.vue'], resolve)
    },
    {
        path: '/component11',
        name: 'component11',
        meta: { title: 'Page11' },
        component: resolve => require(['@/components/website/component11.vue'], resolve)
    },
    {
        path: '/component12',
        name: 'component12',
        meta: { title: 'Page12' },
        component: resolve => require(['@/components/website/component12.vue'], resolve)
    },
    {
        path: '/component13',
        name: 'component13',
        meta: { title: 'Page13' },
        component: resolve => require(['@/components/website/component13.vue'], resolve)
    },
    {
        path: '/component14',
        name: 'component14',
        meta: { title: 'Page14' },
        component: resolve => require(['@/components/website/component14.vue'], resolve)
    },
    {
        path: '/component15',
        name: 'component15',
        meta: { title: 'Page15' },
        component: resolve => require(['@/components/website/component15.vue'], resolve)
    },
    {
        path: '/component16',
        name: 'component16',
        meta: { title: 'Page16' },
        component: resolve => require(['@/components/website/component16.vue'], resolve)
    },
    {
        path: '/component17',
        name: 'component17',
        meta: { title: 'Page17' },
        component: resolve => require(['@/components/website/component17.vue'], resolve)
    },
    {
        path: '/component18',
        name: 'component18',
        meta: { title: 'Page18' },
        component: resolve => require(['@/components/website/component18.vue'], resolve)
    },
    {
        path: '/component19',
        name: 'component19',
        meta: { title: 'Page19' },
        component: resolve => require(['@/components/website/component19.vue'], resolve)
    }
    //#endregion ================================================================================================
]