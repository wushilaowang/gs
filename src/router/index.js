import Vue from 'vue'
import VueRouter from 'vue-router'

import Login from '../views/login.vue'
const login = () => import ('../views/login.vue')
const index = () => import ('../views/index.vue')
const appraisal = () => import ('../views/indexContent/appraisal.vue')
const district = () => import ('../views/indexContent/district.vue')
const userAuthority = () => import ('../views/indexContent/userAuthority.vue')
const allUser = () => import ('../views/indexContent/allUser.vue')
const cardList = () => import ('../views/indexContent/cardList.vue')
const cardContent = () => import ('../views/indexContent/cardContent.vue')
const appraisalResult = () => import ('../views/indexContent/appraisalResult.vue')
const currentAppraisalInfo = () => import ('../views/indexContent/currentAppraisalInfo.vue')
const distributeCard = () => import ('../views/indexContent/distributeCard.vue')
const user = () => import ('../views/indexContent/user.vue')

const addAppraisal = () => import ('../views/indexContent/addAppraisal.vue')

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    redirect: 'index/appraisalResult'
  },
  {
    path: '/login',
    component: login
  },
  {
    path: '/index',
    component: index,
    children: [
      {
        path: 'addAppreisal',
        name: 'addAppreisal',
        component: addAppraisal
      },
      {
        path: 'appraisal',
        component: appraisal
      },
      {
        path: 'district',
        component: district
      },
      {
        path: 'userAuthority',
        component: userAuthority
      },
      {
        path: 'allUser',
        component: allUser
      },
      {
        path: 'cardList',
        component: cardList
      },
      {
        path: 'cardContent',
        component: cardContent
      },
      {
        path: 'appraisalResult',
        component: appraisalResult
      },
      {
        path: 'currentAppraisalInfo',
        component: currentAppraisalInfo
      },
      {
        path: 'distributeCard',
        component: distributeCard
      },
      {
        path: 'user',
        component: user
      },
    ]
  }
  
]

const router = new VueRouter({
  // mode: 'history',
  routes
})
//重复点菜单栏不报错
const originalPush = VueRouter.prototype.push
   VueRouter.prototype.push = function push(location) {
   return originalPush.call(this, location).catch(err => err)
}
//获取token
router.beforeEach((to, from, next) => {
  if(to.path == '/login') {
    return next();
  }
  const token = JSON.parse(window.sessionStorage.getItem('state'));
  console.log(token)
  
  if(!token) {
    // console.log(token.loginInfo == {})
    console.log(2)
    return next({path: '/login'})
  }
  next();
})

export default router
