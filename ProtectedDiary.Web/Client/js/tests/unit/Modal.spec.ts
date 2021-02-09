import { shallowMount, Wrapper } from '@vue/test-utils';
import ModalComponent from '@/components/Modal.vue';

describe('Modal.vue', () => {
  let wrapper: Wrapper<ModalComponent>;

  beforeEach(() => {
    wrapper = shallowMount(ModalComponent);
  });

  it('オーバーレイをクリックすると閉じる', () => {
    const modalOverlay = wrapper.find('.modal.modal-overlay');
    modalOverlay.trigger('click');
    expect(wrapper.emitted('close')).toBeTruthy();
  });

  it('オーバーレイの中のウィンドウをクリックしても閉じない', () => {
    const modalWindow = wrapper.find('.modal-window');
    modalWindow.trigger('click');
    expect(wrapper.emitted('close')).toBeFalsy();
  });
});