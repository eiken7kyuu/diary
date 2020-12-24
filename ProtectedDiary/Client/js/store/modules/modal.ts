import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import store from '@/store';

export interface IModalState {
  active: boolean
}

@Module({ dynamic: true, store, name: 'modal' })
class Modal extends VuexModule {
  active: boolean = false;

  @Mutation
  private OPEN_MODAL() {
    this.active = true;
  }

  @Mutation
  private CLOSE_MODAL() {
    this.active = false;
  }

  @Action
  public open() {
    this.OPEN_MODAL();
  }

  @Action
  public close() {
    this.CLOSE_MODAL();
  }
}

export const modalModule = getModule(Modal);