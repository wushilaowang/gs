import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const state = sessionStorage.getItem('state') ? JSON.parse(sessionStorage.getItem('state')) : {
  loginInfo: {}
}

export default new Vuex.Store({
  state: state,
  mutations: {
    saveLoginInfo(state, payload) {
      state.loginInfo = payload
    }
  },
  actions: {
  },
  modules: {
  }
})
