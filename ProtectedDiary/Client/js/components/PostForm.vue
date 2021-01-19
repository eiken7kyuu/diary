<template>
  <validation-observer ref="observer">
    <form id="postForm" method="post">
      <div class="title-field">
        <validation-provider
          name="タイトル"
          rules="required|max:100"
          v-slot="{ errors }"
        >
          <input
            class="input-title"
            v-model="title"
            type="text"
            placeholder="タイトル"
            name="Diary.Title"
          />
          <span class="text-danger">{{ errors[0] }}</span>
        </validation-provider>
      </div>

      <div class="content-field">
        <validation-provider name="本文" rules="required" v-slot="{ errors }">
          <editor
            name="Diary.Content"
            :api-key="tinyApiKey"
            :init="{
              language: 'ja',
              height: '95%',
              plugins: 'wordcount, link, autolink',
              toolbar: 'undo redo | bold | link',
              link_assume_external_targets: 'http',
              menubar: false,
              statusbar: false,
              forced_root_block: '',
            }"
            v-model="content"
            placeholder="本文"
            output-format="html"
          >
          </editor>
          <span class="text-danger">{{ errors[0] }}</span>
        </validation-provider>
      </div>

      <input
        name="__RequestVerificationToken"
        type="hidden"
        :value="csrfToken"
      />

      <div class="submit-field">
        <button class="button button-submit" type="submit" @click="submit">
          投稿
        </button>
      </div>
    </form>
  </validation-observer>
</template>

<script lang='ts'>
import { Component, Prop, Vue } from 'vue-property-decorator';
import Editor from '@tinymce/tinymce-vue';
import { ValidationObserver, ValidationProvider } from 'vee-validate';
import Cookies from 'js-cookie';

@Component({
  components: {
    Editor,
    ValidationObserver,
    ValidationProvider
  },
})
export default class PostForm extends Vue {
  $refs!: {
    observer: InstanceType<typeof ValidationObserver>;
  };

  private content = '';
  private title = '';
  private csrfToken = '';

  @Prop({ default: '' })
  pTitle!: string;

  @Prop({ default: '' })
  pContent!: string;

  @Prop({ required: true })
  tinyApiKey!: string

  mounted() {
    this.csrfToken = <string>Cookies.get('XSRF-TOKEN');
    this.title = this.pTitle;
    this.content = this.pContent;
    window.addEventListener('beforeunload', this.confirmSave);
  }

  destroyed() {
    this.rmConfirmSave();
  }

  async submit(event: Event) {
    if (!await this.$refs.observer.validate()) {
      event.preventDefault();
      return;
    }

    this.rmConfirmSave();
  }

  confirmSave(event: BeforeUnloadEvent) {
    event.preventDefault();
    event.returnValue = '';
  }

  rmConfirmSave() {
    window.removeEventListener('beforeunload', this.confirmSave);
  }
}
</script>