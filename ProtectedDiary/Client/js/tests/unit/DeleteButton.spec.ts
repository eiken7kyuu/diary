import { config, shallowMount, Wrapper } from '@vue/test-utils';
import DeleteButton from '@/components/DeleteButton.vue';

config.showDeprecationWarnings = false

describe('DeleteButton.vue', () => {
  let wrapper: Wrapper<DeleteButton>;
  const methods = {
    onClick: jest.fn(),
  };

  beforeEach(() => {
    wrapper = shallowMount(DeleteButton, {
      propsData: {
        diaryId: '1',
        diaryTitle: 'test'
      },
      methods: methods
    });
  });

  it('ボタンのテキスト', () => {
    expect(wrapper.text()).toBe('削除');
  });

  it('削除ボタン クリック', () => {
    wrapper.trigger('click');
    expect(methods.onClick).toHaveBeenCalled();
  });
});