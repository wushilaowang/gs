<template>
    <div>
        <div class="right-inner-inner-top">
            <el-button type="primary" @click="handleBack" size="mini">返回</el-button>
            <el-button type="primary" @click="handleCreateCardScore" size="mini">生成卡片分数</el-button>
            <el-button type="primary" @click="distributeCardDialog=true" size="mini" v-if="$store.state.loginInfo.type!=2">分配卡片</el-button>
        </div>
        <el-table :data="currentAppraisalInfoTable" highlight-current-row height="82vh" border>
            <el-table-column label="测评编号" prop="appraisalCode"></el-table-column>
            <el-table-column label="区域等级">
                <template slot-scope="scope">
                    {{scope.row.diorx==0?'地级':'县级'}}
                </template>
            </el-table-column>
            <el-table-column label="区域名称" prop="districtName"></el-table-column>
            <el-table-column label="区域代码" prop="districtName"></el-table-column>
            <el-table-column label="卡片名称" prop="cardName"></el-table-column>
            <el-table-column label="卡片编码" prop="cardCode"></el-table-column>
            <el-table-column label="卡片最大数量" prop="cardMaxCount"></el-table-column>
            <el-table-column label="已提交数量" prop="cardCurCount"></el-table-column>
            <el-table-column label="分值" prop="cardItemScore"></el-table-column>
        </el-table>

        <el-dialog :visible.sync="distributeCardDialog" width="40%" modal title="分配卡片">
            <distributeCard />
        </el-dialog>
    </div>
</template>

<script>
import distributeCard from './distributeCard'
import {generalGet} from '../../network/general'
export default {
    name: 'currentAppraisalInfo',
    components: {
        distributeCard
    },
    data() {
        return {
            currentAppraisalInfoTable: [],
            distributeCardDialog: false,
        }
    },
    methods: {
        loadAppraisalInfoTable() {
            let that = this;
            let data = {
                url: '/api/GetCardAllot',
                params: {
                    AppraisalCode: this.$route.query.appraisalCode
                }
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.currentAppraisalInfoTable = res.data
            })
        },
        //返回
        handleBack() {
            this.$router.go(-1);
        },
        //生成分数
        handleCreateCardScore() {
            generalGet({url: '/api/CreateCardScore', params: {AppraisalCode: this.$route.query.appraisalCode}}).then((res) => {
                console.log(res)
                if(res.msgCode="OK") {
                    this.$notify({
                        title: '成功',
                        type: 'success'
                    })
                }
            })
        },
    },
    mounted() {
        console.log(this.$route.query)
        this.loadAppraisalInfoTable();
    },
}
</script>

<style scoped>

</style>