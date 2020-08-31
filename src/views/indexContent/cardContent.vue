<template>
    <div>
        <div class="right-inner-inner-top">
            <el-button  tton type="primary" @click="handleBack" size="mini">返回</el-button>
        </div>
        <el-table :data="cardContentTable" highlight-current-row height="82vh" border>
            <el-table-column label="卡片编码" prop="cardCode"></el-table-column>
            <el-table-column label="卡片名称" prop="cardName"></el-table-column>
            <el-table-column label="区域">
                <template slot-scope="scope">
                    {{scope.row.diORx==0?'地级':'县级'}}
                </template>
            </el-table-column>
            <el-table-column label="内容" prop="item"></el-table-column>
            <el-table-column label="分数" prop="score"></el-table-column>

        </el-table>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'cardContent',
    data() {
        return {
            cardContentTable: [],
            currentAppraisalCode: '',
        }
    },
    methods: {
        loadCardContentTable() {
            let that = this;
            let data = {
                url: '/api/ShowCardContent',
                params: {
                    AppraisalCode: this.currentAppraisalCode,
                    diORx: -1
                }
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.cardContentTable = res.data;
            })
        },
        //返回
        handleBack() {
            this.$router.go(-1);
        }
    },
    mounted() {
        this.currentAppraisalCode = this.$route.query
        this.loadCardContentTable();
    },
}
</script>

<style scoped>

</style>