<template>
  <button :class="this.className" type="submit" @click="onClick">削除</button>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class DeleteButton extends Vue {
  @Prop({ default: 'button' })
  className!: string;

  onClick (event: Event): void {
    if (confirm('日記を削除します\nよろしいですか？')) {
      return;
    }

    event.preventDefault();
  }
}
</script>

<style lang="scss" scoped>
button {
  color: #fa3333 !important;
}
</style>