<template>
  <button class="delete-button" type="submit" @click="onClick">日記削除</button>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class DeleteButton extends Vue {
  onClick (event: Event): void {
    if (confirm('日記を削除します\nよろしいですか？')) {
      return;
    }

    event.preventDefault();
  }
}
</script>