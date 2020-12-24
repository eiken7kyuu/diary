<template>
  <Modal @close="close" v-if="active">
    <template #header>
      <div>日記の削除</div>
    </template>
    <template #body>
      <div>「{{ title }}」を削除します。<br />よろしいですか？</div>
    </template>
    <template #footer>
      <div>
        <button @click="close" class="button">キャンセル</button>
        <button @click="deleteDiary" class="button delete">削除する</button>
      </div>
    </template>
  </Modal>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { mapMutations, mapState }  from 'vuex';
import Modal from './Modal.vue'
import { modalModule } from '../store/modules/modal';
import { diaryModule } from '../store/modules/diary';

@Component({
  components: { Modal },
})

export default class DiaryDeleteModal extends Vue {
  get active() {
     return modalModule.active;
   }

  get title() {
    return diaryModule.title;
  }

  get id() {
    return diaryModule.id;
  }

  close() {
    modalModule.close();
    diaryModule.reset();
  }

  async deleteDiary() {
    const res = await diaryModule.delete();
    if (res.status == 200) {
      window.location.href = location.href;
      return;
    }
    console.log(res);
  }
}
</script>

<style lang="scss" scoped>
.delete {
  color: #fa3333;
}

button {
  font-size: 1.5rem;
  width: 130px;
}
</style>