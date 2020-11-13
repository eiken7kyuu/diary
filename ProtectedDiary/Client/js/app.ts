import '../css/index.scss';
import './submenu.ts';
import DeleteButton from './DeleteButton.vue';
import { Vue } from 'vue-property-decorator';
import { validatorGroup, validator } from 'vue-dotnet-validator';

Vue.config.productionTip = false;

Vue.component('validator', validator());

new Vue({
  el: '#app',

  components: {
    'delete-button': DeleteButton,
    'validator-group': validatorGroup,
  }
});