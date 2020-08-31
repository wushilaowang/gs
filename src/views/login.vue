<template>
  <div class="container">
    <div class="top"></div>
    <div class="sys-title">测评系统</div>
    <div class="center">
      <div class="slide">
        <el-carousel height="50rem">
          <el-carousel-item v-for="item in pictureList" :key="item">
            <img :src="item"/>
          </el-carousel-item>
        </el-carousel>
      </div>
      <div class="login-form">
        <el-form :model="form" status-icon :rules="rules" ref="form">
          <el-form-item label="账号" prop="account">
            <el-input v-model="form.account" placeholder="请输入账号"></el-input>
          </el-form-item>
          <el-form-item label="密码" prop="password">
            <el-input v-model="form.password" placeholder="请输入密码"></el-input>
          </el-form-item>
          <el-form-item label="行政区划代码1" prop="districtCode">
            <el-input v-model.number="form.districtCode" placeholder="请输入行政区划代码" autocomplete="off"></el-input>
          </el-form-item>
          <el-form-item label="测评等级">
            <el-radio-group v-model="form.entrance">
              <el-radio label="全国测评">全国测评</el-radio>
              <el-radio label="省级测评">省级测评</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="区域等级">
            <el-radio-group v-model="form.diORx">
              <el-radio label="0">地级</el-radio>
              <el-radio label="1">县级</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item>
            <el-button @click="login('form')">登陆</el-button>
          </el-form-item>
        </el-form>
      </div>
    </div>
  </div>
</template>

 <script>
 import Slide from '../components/slide';
 import {login} from '../network/login';
 export default { 
     name: "login", 
     components: {
       Slide,
     },
     data() { 
         var checkDistrictCode = (rule, value, callback) => {
           console.log(!value)
             if(value !== 0 && value.trim == '') {
                 return callback(new Error('行政区划代码不能为空'))
             }else if(parseFloat(value).toString() == "NaN") {
                 callback(new Error('请输入数字'))
            //  }else if(value.toString().length != 6) {
            //      callback(new Error("请输入六位代码"));
             }else {
                 callback();
             }
         }
         var checkAccount = (rule, value, callback) => {
             if(!value) {
                 return callback(new Error('账号不能为空'))
             }else {
                 callback();
             }
         }
         var checkPassword = (rule, value, callback) => {
             if(!value) {
                 return callback(new Error('密码不能为空'))
             }else {
                 callback()
             }
         }
         return { 
               form: { 
                    account: "admin", 
                    password: "admin", 
                    districtCode: "610103", 
                    entrance: "全国测评", 
                    diORx: "0", 
                }, 
                rules: { 
                    districtCode: [
                        {validator: checkDistrictCode, trigger: 'blur' }
                    ] ,
                    account: [
                        {validator: checkAccount, trigger: 'blur'}
                    ],
                    password: [
                        {validator: checkPassword, trigger: 'blur'}
                    ]
                }, 
                pictureList: [require('../assets/00300035230_e092f172.jpg'), require('../assets/00300035230_e092f173.jpg'), require('../assets/00300035230_e092f174.jpg')]
            } 
    }, 
    methods: { 
      //登陆
      login(form) {  
        let that = this;
        this.$refs[form].validate((valid) => {
          if(valid) {
            login(this.form).then(res => {
                console.log(res)
                if(res.msgCode === "Bad") {
                  //610103
                  that.$notify.error({
                    title: res.errorMsg,
                    duration: 2000
                  })
                }else {
                  if(res.data.type === 2) {
                    that.$notify.error({
                      title: "此账号仅可登陆小程序"
                    })
                  }else {
                    window.sessionStorage.setItem('state', JSON.stringify({'loginInfo': res.data}))
                    that.$store.commit("saveLoginInfo", res.data)
                    that.$router.push({path: '/index/appraisal'})
                  }
                }
            })
          }
        })
      },
    } 
} 
</script>

<style scoped>
  .container {
    display: flex;
    flex-direction: column;
  }
  .top {
    width: 100%;
    height: 4rem;
    background-color: #26468d;
  }
  .sys-title {
    padding: 0 40rem;
    font-size: 4rem;
    font-weight: bold;
    color: #515a6e;
    line-height: 6rem;
  }
  .center {
    display: flex;
    padding: 0 40rem;
  }
  .slide {
    flex: .7;
    background-size: cover;
    border-radius: 1rem;
    margin-right: 1rem;
  }
  .slide img {
    background-size: cover;
  }
  .login-form {
    flex: .25;
    border: .1rem solid #d3d3d3;
    border-radius: 2rem;
    padding: 1rem 2rem;
    min-width: 20rem;
  }

  .el-form-item {
    margin-bottom: 1rem;
  }

  .sys-title {
    margin: 10rem 0 1rem 0;
  }


  .el-carousel__item h3 {
    color: #475669;
    font-size: 14px;
    opacity: 0.75;
    line-height: 150px;
    margin: 0;
  }

  .el-carousel {
    border-radius: .4rem;
  }

  .el-carousel__item:nth-child(2n) {
     background-color: #99a9bf;
  }
  
  .el-carousel__item:nth-child(2n+1) {
     background-color: #d3dce6;
  }
</style>