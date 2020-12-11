<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>

<script>
    export default {
        name: 'app',
        components: {

        }
    };
</script>

<style>
    * {
        margin: 0;
        padding: 0;
        outline: 0px solid red;
    }

    html, body {
        background-color: #F2F2F2;
        width:100% !important;
        height:100% !important;
    }
</style>
