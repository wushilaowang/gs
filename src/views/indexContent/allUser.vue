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
            
        </el-table>
        
            
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'allUser',
    data() {
        return {
            allUserTable: [],
            json_fileds: {
                '账号': 'account',
                '密码': 'password',
                '姓名': 'realName',
                '区域': 'districtName',
            },
        }
    },
    methods: {
        //导出
        exportExcel() {
            
        },
        loadAllUserTable() {
            let that = this;
            let data = {
                url: '/api/ShowAllUser'
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.allUserTable = res.data
            })
        }
    },
    mounted() {
        this.loadAllUserTable();
    },
}
</script>

<style scoped>

</style>