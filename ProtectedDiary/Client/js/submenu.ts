import Vue from 'vue';
const vClickOutside = require('v-click-outside');

Vue.use(vClickOutside);
new Vue({
  el: '#submenu',
  data: {
    isActive: false,
  },

  methods: {
    openSubmenu: function () {
      this.isActive = !this.isActive;
    },
    close: function () {
      this.isActive = false;
    },
  }
});