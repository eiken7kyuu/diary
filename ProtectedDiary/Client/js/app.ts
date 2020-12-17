import '../css/index.scss';
import DeleteButton from './components/DeleteButton.vue';
import ReloadAlert from './components/ReloadAlert.vue';
import SubMenu from './components/SubMenu.vue';
import MenuItem from './components/MenuItem.vue';
import { Vue } from 'vue-property-decorator';
import { validatorGroup, validator } from 'vue-dotnet-validator';

Vue.config.productionTip = false;
Vue.component('validator', validator());

new Vue({
  el: '#root',

  components: {
    'validator-group': validatorGroup,
    'delete-button': DeleteButton,
    'reload-alert': ReloadAlert,
    'sub-menu': SubMenu,
    'menu-item': MenuItem
  },
});