<template>
  <div class="submenu-top">
    <compnent
      :is="tag"
      :class="className"
      @click="openSubmenu"
      v-click-outside="vConfig"
    >
      <slot name="header"></slot>
    </compnent>

    <ul role="list" :class="[`submenu`, allowType]" v-show="isActive">
      <slot name="main"></slot>
    </ul>
  </div>
</template>

<script lang="ts">
import vClickOutside from 'v-click-outside';
import { Component, Prop, Vue } from 'vue-property-decorator';

@Component({
  directives: {
    clickOutside: vClickOutside.directive
  }
})

export default class SubMenu extends Vue {
  private isActive: boolean = false;

  private vConfig: any = {
    handler: this.close,
    events: ['touchstart', 'click'],
  }

  openSubmenu () {
    this.isActive = !this.isActive;
  }

  close (event: Event) {
    // https://github.com/ndelvalle/v-click-outside/issues/90
    // モバイルデバイスの場合、リンクをタッチすると遷移前に閉じてしまうため
    if (event.type === 'touchstart') return;
    this.isActive = false;
  }

  @Prop({ required: true })
  tagName!: string;

  @Prop({ default: '' })
  className!: string;

  @Prop({ default: '' })
  allowType!: string;

  get tag() {
    return this.tagName;
  }
}
</script>

<style lang="scss" scoped>
.submenu-top {
  position: relative;
  margin-right: 2rem;

  > .nav-item {
    margin-right: 0;
  }
}
</style>