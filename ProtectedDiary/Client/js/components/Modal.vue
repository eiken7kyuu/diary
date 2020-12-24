<template>
  <transition name="modal" appear>
    <div class="modal modal-overlay" @click.self="$emit('close')">
      <div class="modal-window">
        <header class="modal-header">
          <slot name="header"></slot>
        </header>

        <div class="modal-body">
          <slot name="body"></slot>
        </div>

        <footer class="modal-footer">
          <slot name="footer"></slot>
        </footer>
      </div>
    </div>
  </transition>
</template>

<style lang="scss" scoped>
.modal {
  font-size: 18px;
  &.modal-overlay {
    display: flex;
    align-items: center;
    justify-content: center;
    position: fixed;
    z-index: 30;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.5);
  }

  &-window {
    background: #fff;
    border-radius: 4px;
    overflow: hidden;
    width: 470px;
  }

  &-header {
    text-align: center;
    border-bottom: 1px solid #f2f2f2;
    padding: 16px;
  }

  &-body {
    text-align: center;
    padding: 16px;
  }

  &-footer {
    padding: 16px;
    text-align: right;
    border-top: 1px solid #f2f2f2;
    border-radius: 0 0 4px 4px;
    background-color: #f7f9f9;
  }
}

.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.2s;

  .modal-window {
    transition: opacity 0.2s, transform 0.2s;
  }
}

.modal-enter,
.modal-leave-to {
  opacity: 0;

  .modal-window {
    opacity: 0;
  }
}

.modal-enter {
  .modal-window {
    transform: perspective(500px) translateZ(-80px);
  }
}
</style>