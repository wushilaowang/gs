<template>
    <div>
        <div class="right-inner-inner-top">
        <download-excel :data="allUserTable" :fields="json_fileds" name="最近一次测评用户.xls">
            <el-button type="warning">导出excel</el-button>
        </download-excel>
        </div>
        <el-table :data="allUserTable" highlight-current-row height="82vh" border>
            <el-table-column label="账号" prop="account"></el-table-column>
            <el-table-column label="密码" prop="password"></el-table-column>
            <el-table-column label="姓名" prop="realName"></el-table-column>
            <el-table-column label="区域等级" prop="districtName">
                <template slot-scope="scope">
                    {{scope.districtName==0?'地级':'县级'}}
                </template>
            </el-table-column>
            <el-table-column label="操作">
                <template slot-scope="scope">
                    <el-button size='small' @click="handleDistributeDistrict(scope)">分配区域</el-button>
                </template>
            </el-table-column>
        </el-table>
        
        <el-dialog v-if="showDistrictTree" :visible.sync="showDistrictTree">
            <districtTree :currentUser="currentUser" @reloadFatherUserTable="reloadFatherUserTable"></districtTree>
        </el-dialog>
            
    </div>
</template>

<script>
import {generalGet} from '../../network/general';

import districtTree from '../../components/districtTree'
export default {
    name: 'allUser',
    components: {
        districtTree,
    },
    data() {
        return {
            showDistrictTree: false,
            allUserTable: [],
            json_fileds: {
                '账号': 'account',
                '密码': 'password',
                '姓名': 'realName',
                '区域': 'districtName',
            },
            currentUser: {}
        }
    },
    methods: {
        //导出
        exportExcel() {
            
        },
        //加载用户数据
        loadAllUserTable() {
            let that = this;
            let data = {
                url: '/api/ShowAllUser',
                params: {entrance: this.$store.state.entrance}
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.allUserTable = res.data;
            })
        },
        //分配区域按钮
        handleDistributeDistrict(scope) {
            this.currentUser = scope.row;
            this.showDistrictTree = true;
        },
        //子组件调用重新加载数据
        reloadFatherUserTable() {
            this.loadAllUserTable()
        }
    },
    mounted() {
        this.loadAllUserTable();
    },
}
</script>

<style scoped>

</style>