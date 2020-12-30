import { config, mount, Wrapper } from '@vue/test-utils';
import SubMenu from '@/components/SubMenu.vue';

config.showDeprecationWarnings = false

describe('SubMenu.vue', () => {
  let wrapper: Wrapper<SubMenu>;
  beforeEach(async () => {
    wrapper = mount(SubMenu, {
      propsData: {
        tagName: 'li',
      },
    });
  });

  it('初期状態はメニュー非表示', () => {
    expect(wrapper.find('ul').isVisible()).toBeFalsy();
    expect(wrapper.vm.$data.isActive).toBeFalsy();
  });

  it('openSubmenu', async () => {
    wrapper.find('div.submenu-top > :first-child').trigger('click');
    await wrapper.vm.$nextTick();
    expect(wrapper.find('ul').isVisible()).toBeTruthy();
  });
});