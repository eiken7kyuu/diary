<template>
  <Modal @close="close" v-show="active">
    <template #header>
      <div>日記の削除</div>
    </template>
    <template #body>
      <div>「{{ title }}」を削除します。<br />よろしいですか？</div>
    </template>
    <template #footer>
      <div>
        <input type="button" @click="close" class="button" value="キャンセル" />
        <input
          type="button"
          @click="deleteDiary"
          class="button delete"
          value="削除する"
        />
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
    setTimeout(diaryModule.reset, 200);
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

input[type="button"] {
  font-size: 1.5rem;
  width: 130px;
}
</style>