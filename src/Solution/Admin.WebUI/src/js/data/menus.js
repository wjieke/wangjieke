export const menus = [
    {
        index: 'Index',
        menuName: '后台管理系统',
        menuIcon: 'el-icon-menu'
    },
    {
        index: 'BasicInfo',
        menuName: '基础信息管理',
        menuIcon: 'el-icon-document',
        children: [
            {
                index: 'Address',
                menuName: '地址管理',
                children: [
                    {
                        index: 'Residential',
                        menuName: '小区管理',
                        children: [
                            {
                                index: 'aaa',
                                menuName: 'AAA',
                                //children:[]
                            },
                            {
                                index: 'bbb',
                                menuName: 'BBB',
                                //children: []
                            }
                        ]
                    }
                ]
            }
        ]
    },
    {
        index: 'System',
        menuName: '平台系统管理',
        menuIcon: 'el-icon-document',
        children: [
            {
                index: 'User',
                menuName: '用户管理'
            },
            {
                index: 'Role',
                menuName: '角色管理'
            },
            {
                index: 'Permission',
                menuName: '权限管理'
            },
            {
                index: 'Company',
                menuName: '公司管理'
            },
            {
                index: 'Department',
                menuName: '部门管理'
            },
            {
                index: 'Area',
                menuName: '地区管理'
            },
            {
                index: 'Menu',
                menuName: '菜单管理'
            }
        ]
    },
    {
        index: 'Blog',
        menuName: '博客网站管理',
        menuIcon: 'el-icon-document',
        children: [
            {
                index: 'Article',
                menuName: '文章管理'
            }
        ]
    },
    {
        index: 'Site',
        menuName: '官网网站管理',
        menuIcon: 'el-icon-document',
        children: [
            {
                index: 'NewsType',
                menuName: '新闻类型'
            },
            {
                index: 'News',
                menuName: '新闻管理'
            }
        ]
    },
    {
        index: 'Other',
        menuName: '其他模块管理',
        menuIcon: 'el-icon-document',
        children: [
            {
                index: 'component01',
                menuName: 'Page01'
            },
            {
                index: 'component02',
                menuName: 'Page02'
            },
            {
                index: 'component03',
                menuName: 'Page03'
            },
            {
                index: 'component04',
                menuName: 'Page04'
            },
            {
                index: 'component05',
                menuName: 'Page05'
            },
            {
                index: 'component06',
                menuName: 'Page06'
            },
            {
                index: 'component07',
                menuName: 'Page07'
            },
            {
                index: 'component08',
                menuName: 'Page08'
            },
            {
                index: 'component09',
                menuName: 'Page09'
            },
            {
                index: 'component10',
                menuName: 'Page10'
            },
            {
                index: 'component11',
                menuName: 'Page11'
            },
            {
                index: 'component12',
                menuName: 'Page12'
            },
            {
                index: 'component13',
                menuName: 'Page13'
            },
            {
                index: 'component14',
                menuName: 'Page14'
            },
            {
                index: 'component15',
                menuName: 'Page15'
            },
            {
                index: 'component16',
                menuName: 'Page16'
            },
            {
                index: 'component17',
                menuName: 'Page17'
            },
            {
                index: 'component18',
                menuName: 'Page18'
            },
            {
                index: 'component19',
                menuName: 'Page19'
            },
            {
                index: 'component20',
                menuName: 'Page20'
            }
        ]
    }
]