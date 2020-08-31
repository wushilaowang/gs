<template>
    <div>
        <el-form class="demo-form-inline" :inline="true">
            <el-form-item label="地区:">
                <el-dropdown @command="clickDistrictMenu" placement="top" size="medium">
                    <span  class="el-dropdown-link">
                        <span>{{districtChose.text}}</span>
                        <i class="el-icon-arrow-down el-icon--right"></i>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="(item,index) in districtList" :key="index" :command="item.districtCode+'+'+item.districtName">{{item.districtName}}</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </el-form-item>
            <el-form-item label="卡片:">
                <el-dropdown @command="clickCardMenu" placement="top">
                    <span  class="el-dropdown-link">
                        {{cardChose.text}}<i class="el-icon-arrow-down el-icon--right"></i>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="(item,index) in cardList" :key="index" :command="item.cardCode+'+'+item.cardName">{{item.cardName}}</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </el-form-item>
            <el-form-item label="数量:">
                <el-input size="mini" v-model="cardNum"></el-input>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" size="small" @click="submitStributeCard">提交</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'distributeCard',
    data() {
        return {
            cardList: [],
            districtList: [],
            cardChose: {
                text: '请选择卡片',
                value: 0,
            },
            districtChose: {
                text: '请选择地区',
                value: 0
            },
            cardNum: '',
        }
    },
    methods: {
        //点击卡片
        clickCardMenu(value) {
            console.log(value)
            let valueArr = value.split("+");
            console.log(valueArr)
            this.cardChose.text = valueArr[1];
            this.cardChose.value = valueArr[0];
        },
        //点击区域
        clickDistrictMenu(value) {
            console.log(value)
            let valueArr = value.split("+");
            
            console.log(valueArr[0])
            this.districtChose.text = valueArr[1];
            this.districtChose.value = valueArr[0];
        },
        //获取区域,卡片数据
        getData() {
            let that = this;
            generalGet({url: '/api/ShowCardList'}).then((res) => {
                if(res.msgCode == "OK"){
                    console.log(res);
                    that.cardList = res.data;
                }
            })
            generalGet({url: '/api/ShowAlldistrict'}).then((res) => {
                if(res.msgCode == "OK"){
                    console.log(res);
                    that.districtList = res.data;
                }
            })
        },
        //提交分配卡片数量
        submitStributeCard() {
            let that = this;
            if(this.districtChose.value == 0) {
                this.$notify.error({
                    title: '请选择地区'
                })
            }else if(this.cardChose.value == 0) {
                this.$notify.error({
                    title: '请选择卡片'
                })
            }else if(this.cardNum == '') {
                this.$notify.error({
                    title: '请填写卡片数量'
                })
            }else {
                let data = {
                    url: '/api/SetCardCount',
                    params: {
                        districtCode: this.districtChose.value,
                        districtName: this.districtChose.text,
                        cardCode: this.cardChose.value,
                        cardName: this.cardChose.text,
                        cardMaxCount: this.cardNum,
                    }
                }
                console.log(data.params)
                generalGet(data).then((res) => {
                    console.log(res)
                    if(res.msgCode == "OK"){
                        that.cardChose = {
                            text: '请选择卡片',
                            value: 0,
                        },
                        that.districtChose = {
                            text: '请选择地区',
                            value: 0
                        },
                        that.cardNum = '',
                        that.$notify({
                            title: '卡片分配成功'
                        })
                    }else {
                        thta.$notify({
                            title: '分配失败'
                        })
                    }
                })
            }
        }
    },
    mounted() {
        this.getData();
    }
}
</script>

<style scoped>
    .el-dropdown-link, .el-dropdown {
        width: 24rem;
    }
    .el-dropdown-link {
        cursor: pointer;
        color: #409EFF;
        overflow: hidden;
    }

    .el-icon-arrow-down {
        font-size: 12px;
    }
    .el-dropdown-menu {
        height: 50vh;
        overflow: scroll;
    }
    ::-webkit-scrollbar{
        display:none;
    }
</style>