import { Module, VuexModule, Mutation, Action, getModule } from 'vuex-module-decorators';
import store from '@/store';
import { deleteDiary } from '@/api/diaries';

export interface IDiaryState {
  id: string,
  title: string
}
@Module({ dynamic: true, store, name: 'diary' })
class Diary extends VuexModule implements IDiaryState {
  id: string = '';
  title: string = '';

  @Mutation
  private SET_ID(id: string) {
    this.id = id;
  }

  @Mutation
  private SET_TITLE(title: string) {
    this.title = title;
  }

  @Mutation
  private RESET() {
    this.title = '';
    this.id = '';
  }

  @Action
  public reset() {
    this.RESET();
  }

  @Action
  public addDiaryInfo(diaryInfo: { id: string, title: string }) {
    const { id, title } = diaryInfo;
    this.SET_ID(id);
    this.SET_TITLE(title);
  }

  @Action
  public async delete(): Promise<Response> {
    return await deleteDiary(this.id);
  }
}

export const diaryModule = getModule(Diary);