<template>
  <button :class="this.className" type="button" @click="onClick">削除</button>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { mapMutations, mapState } from 'vuex';
import { diaryModule } from '../store/modules/diary';
import { modalModule } from '../store/modules/modal';

@Component
export default class DeleteButton extends Vue {
  @Prop({ default: 'button' })
  className!: string;

  @Prop({ required: true })
  diaryId!: string;

  @Prop({ required: true })
  diaryTitle!: string;

  onClick (event: Event): void {
    modalModule.open();
    diaryModule.addDiaryInfo({ id: this.diaryId, title: this.diaryTitle });
  }
}
</script>

<style lang="scss" scoped>
button {
  color: #fa3333 !important;
}
</style>