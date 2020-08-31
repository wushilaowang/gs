<template>
    <div class="index-container">
        <div class="top">
            <div class="top-left">
                <div>测评系统后台</div>
            </div>
            <div class="top-right">
                <div>
                    <div class="right-item">
                        <i class="el-icon-switch-button"></i><span icon="el-icon-switch-button" @click="handleLoginOut">退出</span>
                    </div>
                    <div></div>
                </div>
            </div>
        </div>
        <div class="content">
            <div class="content-left">
                <div class="left-inner">
                    <el-row class="tac">
                    <el-col :span="120">
                        <el-menu  :default-active="this.$router.path" class="el-menu-vertical-demo" router>
                            <el-menu-item v-for="(item,i) in leftMenu" :key="i" :route="item.value" :index="item.value">
                                <i :class="'el-icon-'+item.icon"></i>
                                <span slot="title">{{item.text}}</span>
                            </el-menu-item>
                        </el-menu>
                    </el-col>
                    </el-row>
                    <!-- <ul>
                        <li><router-link to="/index/appreisal">测评管理</router-link></li>
                        <li><router-link to="/index/showdata">数据展示</router-link></li>
                        <li><router-link to="/index/district">区域管理</router-link></li>
                        <li><router-link to="/index/userAuthority">用户权限管理</router-link></li>
                        <li><router-link to="/index/allUser">最近一次测评用户</router-link></li>
                        <li><router-link to="/index/cardList">最近一次测评卡片列表</router-link></li>
                        <li><router-link to="/index/cardContent">最近一次测评卡片内容</router-link></li>
                        <li><router-link to="/index/appraisalResult">最近一次测评结果</router-link></li>
                        <li><router-link to="/index/currentAppraisalInfo">测评信息</router-link></li>
                        <li><router-link to="/index/distributeCard">分配卡片</router-link></li>
                        <li><router-link to="/index/user">用户管理</router-link></li>
                    </ul> -->
                </div>
            </div>
            <div class="content-right">
                <div class="right-inner">
                    <div class="right-inner-inner">
                        <router-view></router-view>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import global from '../network/global_variable'
export default {
    name: 'index',
    data() {
        return {
            leftMenu: []
        }
    },
    methods: {
        addAppraisal() {

        },
        handleLoginOut() {
            let that = this;
            window.sessionStorage.removeItem('state', {});
            this.$store.commit('saveLoginInfo', {});
            this.$router.push('/login');
        }
    },
    mounted() {
        this.leftMenu = global.leftMenu;
    }
}
</script>

<style scoped>
    ul, li {
        margin: 0;
        padding: 0;
    }
    li {
        list-style: none;
    }
    .top {
        height: 8vh;
        background-color: #267bb9;
        display: flex;
    }
    .top-left {
        height: 100%;
        flex: .8;
        position: relative;
    }
    .top-left div {
        position: absolute;
        color: #fff;
        font-size: 3rem;
        left: 2rem;
        top: 50%;
        transform: translate(0, -50%);
    }
    .top-right {
        flex: .2;
        height: 100%;
        position: relative;
    }
    .right-item {
        color: white;
        font-size: 1.6rem;
        position: absolute;
        cursor: pointer;
        height: 100%;
        /* transform: translate(0, -50%); */
        top: 30%;
        right: 2rem;
    }
    .right-item:hover {
        color: yellow;
    }
    .content {
        height: 92vh;
        background-color: #d2e4f0;
        display: flex;
    }
    .content-left {
        padding: 1rem 0 1rem 1rem;
    }
    .left-inner {
        background-color: #fff;
        height: 100%;
        border-radius: .5rem;
    }
    .content-right {
        flex-grow: 1;
        padding: 1rem;
        width: 80vw;
    }
    .right-inner {
        height: 100%;
        background-color: #fff;
        border-radius: .5rem;
    }
    .right-inner-inner {
        padding: 1rem;
    }
</style>