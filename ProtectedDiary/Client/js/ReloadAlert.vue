<template></template>

<script lang='ts'>
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class ReloadAlert extends Vue {
  mounted() {
    window.addEventListener('beforeunload', this.confirmSave);

    document.getElementById('postForm').addEventListener('submit', (event: Event) => {
      const dTitle = <HTMLInputElement>document.getElementById('Diary_Title');
      const dContent = <HTMLTextAreaElement>document.getElementById('Diary_Content');
      if (dTitle.value != '' && dContent.value != '') {
        window.removeEventListener('beforeunload', this.confirmSave);
      }
    });
  }

  destroyed() {
    window.removeEventListener('beforeunload', this.confirmSave);
  }

  confirmSave(event: BeforeUnloadEvent) {
    event.preventDefault();
    event.returnValue = '';
  }
}
</script>