import { config, mount, Wrapper } from '@vue/test-utils';
import DiaryDeleteModal from '@/components/DiaryDeleteModal.vue';

config.showDeprecationWarnings = false

describe('DiaryDeleteModal.vue', () => {
  let wrapper: Wrapper<DiaryDeleteModal>;
  const methods = {
    close: jest.fn(),
    deleteDiary: jest.fn()
  };

  const computed = {
    active: jest.fn(() => true)
  };

  beforeEach(() => {
    wrapper = mount(DiaryDeleteModal, {
      computed: computed,
      methods: methods
    });
  });

  it('activeがfalseのときコンポーネントが表示されない', () => {
    wrapper = mount(DiaryDeleteModal, {
      computed: {
        active: jest.fn(() => false)
      },
    });

    expect(wrapper.isVisible()).toBeFalsy();
  });

  it('削除ボタン クリック', () => {
    const deleteButton = wrapper.find('input[type="button"][value="削除する"]');
    deleteButton.trigger('click');
    expect(methods.deleteDiary).toHaveBeenCalled();
  });

  it('キャンセルボタン クリック', () => {
    const cancelButton = wrapper.find('input[type="button"][value="キャンセル"]');
    cancelButton.trigger('click');
    expect(methods.close).toHaveBeenCalled();
  });
});