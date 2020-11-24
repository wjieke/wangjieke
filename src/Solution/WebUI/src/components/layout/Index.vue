<template>
    <div class="index">
        <div class="introduce">
            <h1>瞎扯淡后台管理系统(系统首页_简介)</h1>
            <p>
                瞎扯淡后台管理系统，是一个踩在巨人肩膀上的作品。<br />
                前端使用<b>Vue + ElementUI</b>基于<b>node、vue-cli、webpack</b>的一整套框架。<br />
                后端采用<b>Asp.net Core Web Api + Entity Framework Core</b>完全前后端分离架构。<br />
                本系统当前处于龟速开发状态，且很多功能待完善。完全趋于作者兴趣爱好搭建制作。<br />
            </p>
            <p><b>当前版本：</b>v1.0.0</p>
            <p><b>作者：</b>wangjieke</p>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'IndexTemplate',
        data() {
            return {

            }
        },
        components: {},
        created: function () { },
        mounted: function () { },
        methods: {},
        computed: {},
        watch: {}
    };
</script>

<style scoped>
    .index {
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }

    .introduce {
        text-align: left;
        font-family: "open sans", "Helvetica Neue", Helvetica, Arial, sans-serif;
    }

    h1 {
        font-size: 26px;
        font-weight: 100;
    }

    p {
        font-size: 13px;
        color: #676a6c;
        margin-top: 10px;
    }

    b {
        font-weight: 700;
        font-size: 13px;
        color: #676a6c;
    }
</style>