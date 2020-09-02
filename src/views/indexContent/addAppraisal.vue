<template>
    <div>
        <el-form :inline="true" v-if="showAppraisal" :model="appraisalForm" :rules="formRule" ref="appraisalForm" label-width="12rem">
            <el-form-item label="总分" prop="totalScore">
                <el-input clearable v-model="appraisalForm.totalScore" placeholder="请输入总分"/>
            </el-form-item>
            <el-form-item label="类型">
                <el-radio-group v-model="appraisalForm.scoreType">
                    <el-radio label="问卷">问卷</el-radio>
                    <el-radio label="卡片">卡片</el-radio>
                </el-radio-group>
            </el-form-item>
            <div class="cardList" v-for="(item, index) in cardNum" :key="index">
                <el-form-item label="卡片名称">
                    <el-input class="cardNameInput" @focus="handleFocus" :id="index + 1" clearable v-model="cardName[index]" placeholder="请输入卡片名称"/>
                </el-form-item>
                <el-form-item label="选项数量">
                    <el-input clearable v-model="cardItems[index]" :disabled="cardItemsDisabled[index]" placeholder="请输入卡片数量"/>
                </el-form-item>
            </div>
            <el-form-item>
                <el-button @click="handleCardContentNext">下一步</el-button>
            </el-form-item>
            <el-form-item>
                <el-button @click="submitAppraisalForm('appraisalForm')">提交</el-button>
            </el-form-item>
        </el-form>


        
        <el-form v-if="showCardContent" :inline="true" :model="cardContentForm">
            <div v-for="(item, index) in cardName" :key="index">
                <el-form-item label="卡片内容">
                    <el-input v-model="cardContentForm.item"></el-input>
                </el-form-item>
                <el-form-item label="备注">
                    <el-input v-model="cardContentForm.beizhu"></el-input>
                </el-form-item>
                <el-form-item label="分值">
                    <el-input v-model="cardContentForm.beizhu"></el-input>
                </el-form-item>
                <el-button @click="handleAppraisalBack">上一步</el-button>
                <el-button @click="handleSetCardNumNext">下一步</el-button>
            </div>
        </el-form>
        <div v-if="showSetCardNum">
            SetCardNum
            <el-tree></el-tree>
            
            <el-button @click="handleCardContentBack">上一步</el-button>
            <el-button @click="handleSetUserNext">下一步</el-button>
        </div>
        <div v-if="showSetUser">
            SetUser
            <el-button @click="handleSetCardNumBack">上一步</el-button>
            <el-button @click="handleSetUserNext">分配用户</el-button>
        </div>
    </div>
</template>

<script>
import {generalGet} from '../../network/general';

import addCardContent from './cardContent'
export default {
    name: 'addAppraiasal',
    components: {
        addCardContent
    },
    data() {
        var checkTotalScore = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("总分不能为空"))
            }else if(parseFloat(value).toString() == "NaN"){
                return callback(new Error("请输入数字"));
            }else {
                callback();
            }
        };
        var cardName = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("卡片名称不能为空"));
            }else {
                callback();
            }
        }
        
        return {
            cardName: [],
            cardItems: [],
            cardItemsDisabled: [true],
            //
            cardNum: 1,
            //第二页卡片
            cardContentForm: {
                cardCode: '',
                item: '',
                beizhu: '',
                cardName: '',
                score: '',//备用可不填写
                k50: '',//记录问卷达标标准1，比如问卷这一项90%以上是优秀则此处记录0.9，普通卡片不填
                k51: '',//记录问卷达标标准2，比如问卷这一项70%以上是合格则此处记录0.7，普通卡片不填
                multisign: 0,//是否多用户测评,默认为1（是）
                diORx: '0',
            },
            //第一页测评
            appraisalForm:{
                totalScore: "40",
                scoreType: "问卷",
            },
            formRule: {
                totalScore: [
                    {validator: checkTotalScore, triggle: "blur"}
                ],
                cardName: [],
                cardItems: [],
            },
            showAppraisal: true,
            showCardContent: false,
            showSetCardNum: false,
            showSetUser: false,
        }
    },
    methods: {
        //重置data
        resetData() {
            Object.assign(this.$data, this.$options.data.call(this))
        },
        //input聚焦加一行
        handleFocus(e) {
            console.log(e)
            console.log(e.target.id)
            if(e.target.id == this.cardNum) {
                this.cardItemsDisabled[this.cardNum - 1] = false;
                this.cardItemsDisabled.push(true)
                this.cardNum += 1;
                console.log(this.cardItemsDisabled)
            }
        },
        //下一步填卡片内容
        handleCardContentNext() {
            //判断是否填完整
            let cardListFlag = false;
            console.log(this.cardName)
            console.log(this.cardItems)
            console.log(this.cardNum)
            if(this.cardName.length == 0 || this.appraisalForm.totalScore.trim() == '' || this.cardName.length < this.cardNum - 1 || this.cardItems.length < this.cardNum -1) {
                this.$notify.error({
                    title: '请填写完整'
                })
            }else {
                this.showAppraisal = false;
                this.showCardContent = true;
            }
        },
        //上一步测评
        handleAppraisalBack() {
            this.showCardContent = false;
            this.showAppraisal = true;
            
        },
        //下一步设置卡片数量
        handleSetCardNumNext() {
            this.showCardContent = false;
            this.showSetCardNum = true;
        },
        //上一步设置卡片内容
        handleCardContentBack() {
            this.showCardContent = true;
            this.showSetCardNum = false;
        },
        //下一步设置用户
        handleSetUserNext() {
            this.showSetCardNum = false;
            this.showSetUser = true;
        },
        //上一步设置卡片数量
        handleSetCardNumBack() {
            this.showSetUser = false;
            this.showSetCardNum = true;
        },



        //
        submitAppraisalForm(appraisalForm) {
            let that = this;
            let appraisalNameData = {
                url: '/api/StartAppraisal',
                params: {AppraisalName: that.$store.state.entrance}
            }
            let totalScoreData = {
                url: '/api/SetSumScore',
                params: {score: that.appraisalForm.totalScore, scoreType: that.appraisalForm.scoreType, entrance: that.$store.state.entrance}
            }
            this.$refs[appraisalForm].validate((valid) => {
                if(!valid) {
                    console.log(valid)
                }else {
                    generalGet(appraisalNameData).then((res) => {
                        if(res.msgCode == "OK") {
                            console.log(res)
                            return generalGet(totalScoreData);
                        }
                    }).then((res2) => {
                        if(res2.msgCode == "OK") {
                            console.log(res2)
                        }
                    }).catch(err => {
                        console.log(err)
                    })
                }
            })
        }
    }
}
</script>

<style scoped>
</style>