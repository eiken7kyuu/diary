import Vue from 'vue';
import Vuex from 'vuex';
import { IDiaryState } from './modules/diary';
import { IModalState } from './modules/modal';

Vue.use(Vuex);

export interface IRootState {
  diary: IDiaryState,
  modal: IModalState
}

export default new Vuex.Store<IRootState>({});