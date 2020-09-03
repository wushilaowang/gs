<template>
    <div>
        <!-- 测评 -->
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

        <!-- 卡片内容 -->
        <el-form v-if="showCardContent" :inline="true" :model="cardContentForm" label-width="8rem ">
            <el-form-item label="卡片名">
            <el-dropdown @command="handleCardChange">
                <span class="el-dropdown-link">
                    {{currentCard.name}}<i class="el-icon-arrow-down el-icon--right"></i>
                </span>
                <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item v-for="(item,index) in cardName" :key="index" :command="{index:index,name:item}">{{item}} </el-dropdown-item>
                </el-dropdown-menu>
            </el-dropdown>
            </el-form-item>
            <div v-for="(item,index) in cardArr[currentCard.index].items" :key="index">
                <el-form-item :label="'内容'+(index + 1)" >
                    <el-input v-model="item.content"></el-input>
                </el-form-item>
                <el-form-item label="备注">
                    <el-input v-model="item.mark"></el-input>
                </el-form-item>
                <el-form-item label="分值">
                    <el-input v-model="item.score"></el-input>
                </el-form-item>
            </div>
            <el-button @click="handleAppraisalBack">上一步</el-button>
            <el-button @click="handleSetCardNumNext">下一步</el-button>
        </el-form>

        <!--  -->
        <div v-if="showSetCardNum">
            SetCardNum
            <el-tree :props="props"  :data="districtList"  node-key="id" highlight-current="true"></el-tree>
            
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
import {getDistrict} from '../../network/getDistricts'

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
            props: {
                label: 'title',
                children: 'children'
            },
            districtList: [],
            cardArr:[],
            currentCard: {index: 0, name: ''},
            cardName: [],
            cardItems: [],
            cardItemsDisabled: [true],

            cardContent: [],
            cardMark: [],
            cardScore: [],
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
        //切换卡片
        handleCardChange(e) {
            this.currentCard = e;
        },
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
            
            //设置当前卡片为第一张卡片
            this.currentCard.index = 0;
            this.currentCard.name = this.cardName[0];
            //将卡片名和卡片选项空值放入变量
            let cardContainer = [];
            this.cardName.map((item, index) => {
                cardContainer.push({name: item, items: []})
            })
            this.cardItems.map((item, index) => {
                for(let i = 0; i < item; i++) {
                    cardContainer[index].items.push({content: '', mark: '', score: ''});
                }
            })
            //将之前卡片保存在变量
            let cardTemp = this.cardArr;
            //判断是否已经填过卡片数据
            if(cardTemp.length != 0) {
                //将temp中数据转到cardContent
                cardContainer.map((card, index) => {
                    card.items.map((item, index2) => {
                        cardTemp.map((item2, index3) => {
                            item2.items.map((item3, index4) => {
                                if(index == index3 && index2 == index4) {
                                    console.log(item+ '' + item3)
                                    item.content = item3.content
                                    item.mark = item3.mark
                                    item.score = item3.score
                                }
                            })
                        })
                    })
                })
            }
            //赋到data
            this.cardArr = cardContainer;
            //判断是否填完整
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
            console.log(this.cardArr)
            console.log(this.cardItems)
            console.log(this.cardContent)
            //卡片内容,备注,分数不为空的数量
            let cardContentNum = 0, cardMarkNum = 0, cardScoreNum = 0;
            this.cardArr.map((item) => {
                item.items.map(item2 => {
                    if(item2.content != '' && typeof(item2.content)!="undefined") {
                        cardContentNum += 1;
                    }
                    if(item2.mark != '' && typeof(item2.mark)!="undefined") {
                        cardMarkNum += 1;
                    }
                    if(item2.score != '' && typeof(item2.score)!="undefined") {
                        cardScoreNum += 1;
                    }
                    console.log(item2)
                })
            })
            console.log('' + cardContentNum + cardMarkNum + cardScoreNum)
            


            let cardItemsNum = this.cardItems.reduce((pre, cur) => {
                return parseInt(pre) + parseInt(cur)
            })
            if(cardContentNum < cardItemsNum || cardMarkNum < cardItemsNum || cardScoreNum < cardItemsNum) {
                this.$notify.error({
                    title: '请填写完整'
                })
            }else {
                this.showCardContent = false;
                this.showSetCardNum = true;
            }
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

        //获取地址
        getDistrictList() {
            generalGet({url: '/api/ShowAlldistrict'}).then(res => {
                let dataList = res.data;
                res.data.push({})
                let currentCity = '';
                let districtList1 = [];
                let childList = [];
                let index = 10000;
                dataList.map(item => {
                    if(currentCity != item.owenerCityName){
                        if(currentCity != ''){
                            districtList1.push({id: index, title: currentCity, children: childList});
                            childList = [];
                        }
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parrentId: index + 1});
                        currentCity = item.owenerCityName;
                        index ++;
                    }else {
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parrentId: index})
                    }
                })
                this.districtList = districtList1;
            })
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
    },
    mounted() {
        //获取地址
        this.getDistrictList();
    }
}
</script>

<style scoped>
    .el-dropdown-link {
        cursor: pointer;
        color: #409EFF;
    }
    .el-icon-arrow-down {
        font-size: 12px;
    }
</style>