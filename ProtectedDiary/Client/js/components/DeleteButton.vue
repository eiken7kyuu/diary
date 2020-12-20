<template>
  <button :class="this.className" type="button" @click="onClick">削除</button>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component
export default class DeleteButton extends Vue {
  @Prop({ default: 'button' })
  className!: string;

  @Prop({ required: true })
  csrfToken!: string;

  @Prop({ required: true })
  diaryId!: string;

  async onClick (event: Event): Promise<void> {
    if (confirm('日記を削除します\nよろしいですか？')) {
        const result = await fetch(`/diaries/${this.diaryId}/delete`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'X-CSRF-TOKEN': this.csrfToken
          },
        });

        window.location.href = result.url;
    }
  }
}
</script>

<style lang="scss" scoped>
button {
  color: #fa3333 !important;
}
</style>