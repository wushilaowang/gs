<template>
    <div class="">
        <div v-if="$store.state.loginInfo.type!=2" class="right-inner-inner-top">
            <el-button round type="primary" size="small" @click="addAppraisalDialog=true">添加测评</el-button>
        </div>
        <el-table :data="appraisalTable" highlight-current-row height="82vh" border>
            <el-table-column label="测评编号" prop="appraisalCode"></el-table-column>
            <el-table-column label="测评名称" prop="appraisalName"></el-table-column>
            <el-table-column label="测评类型">
                <template slot-scope="scope">
                    {{scope.row.appraisalType==0?'全国':'省级'}}
                </template>
            </el-table-column>
            <el-table-column label="开始时间">
                <template slot-scope="scope">
                    {{scope.row.starttime.split('T')[0]}} {{scope.row.starttime.split('T')[1]}}
                </template>
            </el-table-column>
            <el-table-column label="结束时间">
                <template slot-scope="scope">
                    <span v-if="scope.endtime">{{scope.row.endtime.split('T')[0]}} {{scope.row.endtime.split('T')[1]}}</span>
                </template>
            </el-table-column>
            <el-table-column min-width="170rem" label="操作">
                <template slot-scope="scope">
                    <el-button @click="handleAdd(scope.row)" size="small" v-if="$store.state.loginInfo.type!=2">添加卡片</el-button>
                    <el-button @click="handleCardContent(scope.row)" size="small">卡片内容</el-button>
                    <el-button size="small" @click="currentAppraisalInfo(scope.row)">卡片分配详情</el-button>
                    <el-button @click="handleAppraisalResult(scope.row)" size="small">测评结果</el-button>
                    <el-button @click="handleDeleteAppraisal(scope.row)" size="small">删除</el-button>
                </template>
            </el-table-column>
        </el-table>

        <el-dialog :visible.sync="addAppraisalDialog" width="40%" modal title="添加测评" @closed="handleCloseAddAppraisal">
            <addAppraisal ref="addAppraisal" />
        </el-dialog>
        <el-dialog :visible.sync="addCardDialog" width="40%" modal title="添加卡片">
            <addCard :currentAppraisalCode="currentAppraisalCode" />
        </el-dialog>
    </div>
</template>

<script>
import {generalGet, generalPost} from '../../network/general'

import addAppraisal from '../../views/indexContent/addAppraisal'
import addCard from '../../views/indexContent/addCard'
export default {
    name: 'appraisal',
    components: {
        addAppraisal,
        addCard
    },
    data() {
        return {
            appraisalTable: [],
            addAppraisalDialog: false,
            addCardDialog: false,
            currentAppraisalCode: '1',
        }
    },
    mounted() {
        this.loadAppraisal()
    },
    methods: {
        //addAppraisal页面重置data
        handleCloseAddAppraisal() {
            this.$refs.addAppraisal.resetData();
        },
        //加载数据
        loadAppraisal() {
            let that = this;
            let data = {
                url: '/api/GetAllAppraisalCode',
                params: {entrance: that.$store.state.entrance}
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.appraisalTable = res.data;
            })
        },
        //添加测评
        handleAdd(row) {
            this.addCardDialog = true;

        },
        //卡片内容
        handleCardContent(row) {
            console.log(row.appraisalCode);
            this.$router.push({
                path: '/index/cardContent', 
                query: {appraisalCode: row.appraisalCode}
            })
        },
        //测评结果
        handleAppraisalResult(row) {
            this.$router.push({
                path: '/index/appraisalResult',
                query: {appraisalCode: row.appraisalCode}
            })
        },
        //当前测评信息
        currentAppraisalInfo(row) {
            this.$router.push({path: '/index/currentAppraisalInfo', query: {appraisalCode: row.appraisalCode}})
        },
        //删除
        handleDeleteAppraisal(row) {
            console.log(row)
            this.$confirm('确认删除测评?', '提示', {
                confirmButtonText: '确认',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                generalPost({url: '/api/deleteAppraisal', params: {'AppraisalName': row.appraisalName.toString()}}).then((res) =>  {
                    if(res) {
                        if(res.msgCode == 'OK') {
                            this.$notify({
                                title: '删除成功'
                            });
                            this.$router.go(0)
                        }
                    }else {
                        this.$notify.error({
                            title:'删除失败'
                        })
                    }
                })
            }).catch(() => {
                this.$message({
                    type: 'info',
                    message: '已取消删除'
                })
            })
            
        }
    }
}
</script>

<style scoped>

</style>