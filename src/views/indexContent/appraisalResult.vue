<template>
    <div>
        <el-dropdown split-button @command="handleAppraisalDropdownChange">
            {{typeof(currentAppraisal.appraisalName) == "undefined"?'请选择测评':currentAppraisal.appraisalName}}
            <el-dropdown-menu slot="dropdown">
                <div v-for="(item,index) in appraisalDropdown" :key="index">
                <el-dropdown-item :command="item">{{item.appraisalName}}</el-dropdown-item>
                </div>
            </el-dropdown-menu>
        </el-dropdown>

        <el-dropdown split-button @command="handleCardDropdownChange">
            {{typeof(currentCard.cardName) == "undefined"?'请选择卡片':currentCard.cardName}}
            <el-dropdown-menu slot="dropdown">
                <div v-for="(item,index) in cardDropdown" :key="index">
                <el-dropdown-item :command="item">{{item.cardName}}</el-dropdown-item>
                </div>
            </el-dropdown-menu>
        </el-dropdown>

        <el-button type="primary" @click="handleAppraisalTotalScore">查看测评总分</el-button>

        <el-table :data="cardDetail" style="width: 100%">
            <el-table-column prop="item" label="内容" width="180">
            </el-table-column>
            <el-table-column prop="beizhu" label="选项" width="180">
            </el-table-column>
            <el-table-column prop="score" label="得分/比例" width="100">
            </el-table-column>
        </el-table>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'appraisalResult',
    computed: {
        appraisalDropdownValue: {
            get() {this.currentAppraisal != '' ? this.currentAppraisal.appraisalName : 'buzhi'},
            set() {
                this.currentAppraisal != '' ? this.currentAppraisal.appraisalName : 'buzhi'
            }
        }
    },
    data() {
        return {
            appraisalDropdown: [],
            currentAppraisal: {},
            cardDropdown: [],
            currentCard: {},
            cardDetail: []
        }
    },
    methods: {
        getAppraisal() {
            let that = this;
            generalGet({url: 'api/GetAllAppraisalCode', params: {entrance: that.$store.state.entrance}}).then(res => {
                console.log(res)
                that.appraisalDropdown = res.data
            })
        },
        //选择测评触发
        handleAppraisalDropdownChange(e) {
            console.log(e)
            this.currentAppraisal = e;
            generalGet({url: '/api/ShowCardList', params: {entrance: this.$store.state.entrance, appraisalCode: e.appraisalCode}}).then(res => {
                console.log(res)
                this.cardDropdown = res.data;
            })
        },
        //选择卡片触发
        handleCardDropdownChange(e) {
            console.log(e)
            let that = this;
            this.currentCard = e;
            generalGet({url: '/api/ShowCardContent', params: {entrance: that.$store.state.entrance, appraisalCode: this.currentAppraisal.appraisalCode}}).then(res => {
                console.log(res)
                that.cardDetail = res.data;
            })
        },
        // 查看总分
        handleAppraisalTotalScore() {
            generalGet({url: '/api/GetCpResults', params: {appraisalCode: this.currentAppraisal.appraisalCode}}).then(res => {
                console.log(res)
            })
        }
    },
    mounted() {
        this.getAppraisal()
    },
}
</script>

<style scoped>

</style>
