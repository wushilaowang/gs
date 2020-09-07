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

        <!-- 为区域分配卡片 -->
        <div v-if="showSetCardNum">
            <div class="flex-box">
                <el-cascader
                    v-model="value"
                    :options="districtList"
                    :props="props"
                    @change="handleCascaderChange">
                </el-cascader>
                <div v-if="currentDistrictCards.card">
                    <div class="flex-box" v-for="(item,index) in currentDistrictCards.card" :key="index">
                        <label>{{item.cardName}}</label>
                        <el-input v-model="item.cardNum" placeholder="请输入卡片数量" :key="index" @change="handleCardNumChange(item)"></el-input>
                    </div>
                </div>
            </div>
            <el-button @click="handleCardContentBack">上一步</el-button>
            <el-button @click="handleSetUserNext">下一步</el-button>
        </div>

        <!--  -->
        <div v-if="showSetUser">
            <div v-for="(item,index) in userForm" :key="index">
                <el-form :inline="true" label-width="7rem">
                    <el-form-item label="用户名">
                        <el-input :id="'user'+index" placeholder="请输入用户名" v-model="item.account" @focus="handleUserAdd"></el-input>
                    </el-form-item>
                    <el-form-item label="密码">
                        <el-input placeholder="请输入密码" v-model="item.password"></el-input>
                    </el-form-item>
                    <el-form-item label="姓名">
                        <el-input placeholder="请输入姓名" v-model="item.name"></el-input>
                    </el-form-item>
                    {{item.type}}
                    <el-form-item label="类型">
                        <el-radio-group v-model="item.type">
                            <el-radio :label="0">系统管理员</el-radio>
                            <el-radio :label="1">测评带队</el-radio>
                            <el-radio :label="2">测评成员</el-radio>
                        </el-radio-group>
                    </el-form-item>
                </el-form>
            </div>
            <el-button @click="handleSetCardNumBack">上一步</el-button>
            <el-button @click="submitAppraisal">提交</el-button>
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
            currentDistrictCards: [],//当前选择区域的卡片
            value: [],
            props: {
                label: 'title',
                value: 'id',
                children: 'children',
                expandTrigger: 'hover' 
            },
            districtList: [],
            cardArr:[],
            currentCard: {index: 0, name: ''},
            cardName: [],
            cardItems: [],
            cardItemsDisabled: [true],

            cardContent: [],//卡片选项的卡片内容
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
            userForm: [],
            showAppraisal: true,
            showCardContent: false,
            showSetCardNum: false,
            showSetUser: false,
        }
    },
    methods: {
        //根据id判断是否添加用户text框
        handleUserAdd(e) {
            console.log(e)
            console.log(e.target.id)//user0
            let a = e.target.id.split('r');
            if(parseInt(a[1]) + 1 == this.userForm.length) {
                this.userForm.push({account: '', password: '', name: '', type: 2})
            }
        },
        //卡片数量改变
        handleCardNumChange(e) {
            let temp = JSON.parse(JSON.stringify(this.districtList));

            
            console.log(e)
            temp.map(item => {
                if(item.id == this.currentDistrictCards.parentId) {
                    item.children.map(item2 => {
                        if(item2.id == this.currentDistrictCards.id) {
                            item2.card.map(item3 => {
                                if(item3.cardName == e.cardName) {
                                    item3.cardNum = e.cardNum;
                                }
                            })
                        }
                    })
                }
            })
            console.log(temp)
            this.districtList = temp;
        },
        //区域级联改变
        handleCascaderChange(e) {
            console.log(e)
            this.districtList.map(item => {
                if(item.id == e[0]) {
                    item.children.map(item2 => {
                        if(item2.id == e[1]) {
                            let a = JSON.stringify(item2)
                            this.currentDistrictCards = JSON.parse(a)
                            // this.currentDistrictCards = item2
                            console.log(this.currentDistrictCards)
                        }
                    })
                }
            })
        },
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
            //获取地址
            console.log(this.districtList.length)
            this.getDistrictList();
            //设置当前卡片为第一张卡片
            this.currentCard.index = 0;
            this.currentCard.name = this.cardName[0];
            //将卡片名和卡片选项空值放入变量
            let cardContainer = [];
            this.cardName.map((item, index) => {
                let xuhao = (index + 1) < 10 ? '0' + (index + 1) : (index + 1);
                cardContainer.push({name: item, items: [], cardCode: 'K01' + xuhao})
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
            //初始用户
            if(this.userForm.length < 1) {
                this.userForm.push({account: '', password: '', name: '', type: 2})
            }
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
                //
                let card = [];
                this.cardName.map((item, index) => {
                    card.push({cardName: item, cardNum: 0, cardCode: (index + 1) > 9 ? 'K01' + (index +1) : 'K01' + '0' + (index + 1)});
                })
                console.log(card)
                dataList.map(item => {
                    if(currentCity != item.owenerCityName){
                        if(currentCity != ''){
                            districtList1.push({id: index, title: currentCity, children: childList});
                            childList = [];
                        }
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parentId: index + 1, card: card});
                        currentCity = item.owenerCityName;
                        index ++;
                    }else {
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parentId: index, card: card})
                    }
                })
                this.districtList = districtList1;
                console.log(districtList1)
            })
        },


        //提交
        submitAppraisal(appraisalForm) {
            let that = this;
            //创建测评
            let appraisalNameData = {
                url: '/api/StartAppraisal',
                params: {AppraisalName: that.$store.state.entrance}
            }
            //设置总分
            let totalScoreData = {
                url: '/api/SetSumScore',
                params: {score: that.appraisalForm.totalScore, scoreType: that.appraisalForm.scoreType, entrance: that.$store.state.entrance}
            }
            //添加卡片
            let addCard = {
                url: '/api/SetCardList',
                params: {}
            }
            //添加卡片内容
            let addCardDetail = {
                url: '/api/SetCardContent',
                params: {}
            }
            //给区域分配卡片
            let distributeCard = {
                url: '/api/SetCardCount',
                params: {}
            }
            //分配用户
            let addUser = {
                url: '/api/SetUser',
                params: {}
            }
            
            //添加测评
            let a = generalGet(appraisalNameData).then((res) => {
                if(res.msgCode == "OK") {
                    console.log(res)
                    //设置总分
                    return generalGet(totalScoreData);
                }
            }).then((res2) => {
                if(res2.msgCode == "OK") {
                    console.log(res2)
                }
                let requestArr = [];
                this.cardArr.map((item, index) => {
                    let number = (index + 1) < 10 ? '0' + (index + 1) : index + 1
                    addCard.params = {cardName: item.name, cardItemCount: item.items.length, cardCode: item.cardCode, entrance: this.$store.state.entrance}
                    requestArr.push(generalGet(addCard))
                })
                console.log(1)
                //添加卡片
                return Promise.all(requestArr)
            })
            .then(res => {
                console.log(res)
                let requestArr = [];
                let a = this.cardArr.map(item => {
                    item.items.map(item2 => {
                        addCardDetail.params = {cardCode: item.cardCode, item: item2.content, beizhu: item2.mark,cardName: item.name, entrance: this.$store.state.entrance}
                        requestArr.push(generalGet(addCardDetail));
                    })
                })
                //添加卡片内容
                return Promise.all(requestArr)
            }).then(res => {
                console.log(res)
                let requestArr = [];
                this.districtList.map(item => {
                    item.children.map(item2 => {
                        item2.card.map(item3 => {
                            if(item3.cardNum != '') {
                                distributeCard.params = {
                                    districtCode: item2.code, districtName: item2.title, cardCode: item3.cardCode, cardScore: null, cardMaxCount: item3.cardNum, entrance: this.$store.state.entrance
                                }
                                requestArr.push(generalGet(distributeCard))
                            }
                        })
                    })
                })
                //为地区分配卡片
                return Promise.all(requestArr);
            }).then(res => {
                console.log(res)
                let requestArr = [];
                for(let i = 0; i < this.userForm.length - 1; i ++) {
                    addUser.params = {Account: this.userForm[i].account, Password: this.userForm[i].password, RealName: this.userForm[i].name, Type: this.userForm[i].type, entrance: this.$store.state.entrance};
                    requestArr.push(generalGet(addUser))
                }
                //添加用户
                return Promise.all(requestArr)
            }).then(res => {
                console.log(res)
            })
            .catch(err => {
                console.log(err)
            })
        }
    },
    mounted() {
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
    .flex-box {
        display: flex;
    }
</style>