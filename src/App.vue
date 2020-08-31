<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
import Login from './views/login'
export default {
  name: "app",
  components: {
    Login,
  },
  mounted() {
    window.addEventListener('unload', this.saveState);
  },
  methods: {
    saveState() {
      sessionStorage.setItem('state', JSON.stringify(this.$store.state))
    }
  }
}
</script>

<style>
  html {
    font-size: 10px;
  }
  html, body {
    padding: 0;
    margin: 0;
  }
  
  .right-inner-inner-top {
      margin: 0 0 1rem 0;
  }
</style>
