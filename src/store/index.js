import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const state = sessionStorage.getItem('state') ? JSON.parse(sessionStorage.getItem('state')) : {
  loginInfo: {},
  entrance: ''
}

export default new Vuex.Store({
  state: state,
  mutations: {
    saveLoginInfo(state, payload) {
      state.loginInfo = payload
    },
    saveEntrance(state, payload) {
      state.entrance = payload;
    }
  },
  actions: {
  },
  modules: {
  }
})
