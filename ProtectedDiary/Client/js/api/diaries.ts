import Cookies from 'js-cookie';

export const deleteDiary = async (id: string) => {
  const csrfToken = <string>Cookies.get('XSRF-TOKEN');
  return await fetch(`/diaries/${id}/delete`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/x-www-form-urlencoded',
      'X-CSRF-TOKEN': csrfToken
    },
  });
}