<template>
    <div>
        <div class="right-inner-inner-top">
            <el-button type="primary" @click="handleBack" size="mini">返回</el-button>
        </div>
        <el-table :data="appraisalResultTable">
            <el-table-column label="用户" prop="Uploader"></el-table-column>
            <el-table-column label="位置" prop="LocalName"></el-table-column>
            <el-table-column label="区域" prop="Districtname"></el-table-column>
            <el-table-column label="测评编号" prop="Appraisalcode"></el-table-column>
            <el-table-column label="卡片名" prop="Cardname"></el-table-column>
            <el-table-column label="提交时间" prop="writetime"></el-table-column>
        </el-table>
        <div>有数据的县</div>
        <el-table :data="haveScoreDistrictsTable">
            <el-table-column label="区域" prop="Districtname"></el-table-column>
            <el-table-column label="行政区划代码" prop="Districtname"></el-table-column>
        </el-table>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'appraisalResult',
    data() {
        return {
            appraisalResultTable: [],
            haveScoreDistrictsTable: []
        }
    },
    methods: {
        loadAppraisalResultTable() {
            let that = this;
            let data = {
                url: '/api/GetCpResults',
                AppraisalCode: this.$route.query,
                districtCode: ''
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.userAuthorityTable = res.data
            })
            generalGet({url: '/api/GetTheDistricts'}).then((res) => {
                console.log(res);
                that.haveScoreDistrictsTable = res.data
            })
        },
        //返回
        handleBack() {
            this.$router.go(-1);
        }
    },
    mounted() {
        this.loadAppraisalResultTable();
    },
}
</script>

<style scoped>

</style>
