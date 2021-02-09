import '../css/index.scss';
import DeleteButton from '@/components/DeleteButton.vue';
import DiaryDeleteModal from '@/components/DiaryDeleteModal.vue';
import SubMenu from '@/components/SubMenu.vue';
import MenuItem from '@/components/MenuItem.vue';
import PostForm from '@/components/PostForm.vue';
import store from '@/store';
import { localize, extend } from 'vee-validate';
import * as rules from 'vee-validate/dist/rules';
import ja from 'vee-validate/dist/locale/ja.json';
import { Vue } from 'vue-property-decorator';

localize('ja', ja);

for (const [rule, validation] of Object.entries(rules)) {
  extend(rule, {
    ...validation as any
  })
}

Vue.config.productionTip = false;
new Vue({
  el: '#root',
  store,
  components: {
    'delete-button': DeleteButton,
    'sub-menu': SubMenu,
    'menu-item': MenuItem,
    'diary-delete-modal': DiaryDeleteModal,
    'post-form': PostForm,
  },
});