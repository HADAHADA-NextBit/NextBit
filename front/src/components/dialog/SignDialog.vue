<template>
  <q-dialog persistent>
    <q-card
      style="min-width: 320px; max-width: 500px; width: 100%"
      :class="$q.dark.isActive ? 'bg-dark-08' : 'bg-light-02'"
      class="text-white"
    >
      <q-card-section class="row items-center q-pb-none">
        <q-space />
        <q-btn icon="close" flat round dense v-close-popup />
      </q-card-section>

      <q-card-section class="q-pa-none">
        <div class="text-h6 text-center">
          <q-icon name="img:/logo.svg" size="35px" />
        </div>
      </q-card-section>

      <q-card-section class="text-center pt-40 pb-20">
        <q-btn-toggle
          v-model="type"
          glossy
          :toggle-color="$q.dark.isActive ? 'dark-03' : 'purple-01'"
          :options="[
            { label: $t('word.login'), value: 'login' },
            { label: $t('word.signup'), value: 'signup' },
          ]"
          spread
          @update:model-value="formState = setFormState()"
        />
      </q-card-section>

      <q-card-section class="pt-20">
        <q-form @submit="loginSubmit" class="q-gutter-md">
          <q-input
            dark
            rounded
            clearable
            standout="gray-01"
            color="white"
            v-model="formState.identity"
            :label="$t('word.identity')"
            :tabindex="1"
            lazy-rules
            :rules="[
              (val) =>
                (val && $identityExp.test(val)) || $t('input.hint.identity'),
            ]"
          >
            <template v-slot:prepend>
              <q-icon name="person" color="black" />
            </template>
          </q-input>

          <q-input
            dark
            rounded
            clearable
            standout="gray-01"
            v-model="formState.password"
            :label="$t('word.password')"
            :tabindex="1"
            :type="passwordOff ? 'text' : 'password'"
            lazy-rules
            :rules="[
              (val) =>
                (val && $passwordExp.test(val)) || $t('input.hint.password'),
            ]"
          >
            <template v-slot:prepend>
              <q-icon name="lock" color="black" />
            </template>

            <template v-slot:append>
              <q-icon
                :name="passwordOff ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="passwordOff = !passwordOff"
              />
            </template>
          </q-input>

          <q-input
            dark
            rounded
            clearable
            standout="gray-01"
            v-model="formState.confirmPassword"
            :label="$t('word.confirm_password')"
            :tabindex="1"
            :type="confirmPasswordOff ? 'text' : 'password'"
            lazy-rules
            :rules="[
              (val) =>
                (val && $passwordExp.test(val)) ||
                $t('input.hint.confirm_password'),
            ]"
            v-show="type === 'signup'"
          >
            <template v-slot:prepend>
              <q-icon name="lock" color="black" />
            </template>

            <template v-slot:append>
              <q-icon
                :name="confirmPasswordOff ? 'visibility_off' : 'visibility'"
                class="cursor-pointer"
                @click="confirmPasswordOff = !confirmPasswordOff"
              />
            </template>
          </q-input>

          <q-input
            dark
            rounded
            clearable
            standout="gray-01"
            v-model="formState.nickname"
            :label="$t('word.nickname')"
            :tabindex="1"
            lazy-rules
            :rules="[
              (val) =>
                (val && $identityExp.test(val)) || $t('input.hint.identity'),
            ]"
            v-show="type === 'signup'"
          >
            <template v-slot:prepend>
              <q-icon name="person" color="black" />
            </template>
          </q-input>

          <div>
            <q-btn
              :label="$t(type === 'login' ? 'word.login' : 'word.signup')"
              type="submit"
              color="purple-01"
              class="full-width"
              rounded
              padding="19px"
              size="18px"
              glossy
            />
          </div>
        </q-form>
      </q-card-section>

      <q-card-section class="pb-40">
        <div class="text-center relative-position">
          <q-separator
            class="absolute-center"
            style="width: 30%; left: 20%"
            color="gray-01"
          />

          {{ $t(type === 'login' ? 'word.easy_login' : 'word.easy_signup') }}

          <q-separator
            class="absolute-center"
            style="width: 30%; left: 80%"
            color="gray-01"
          />
        </div>

        <div class="row justify-around mt-20">
          <q-btn
            padding="0"
            unelevated
            round
            v-for="platform in easy"
            :key="platform.text"
            class="shadow-10"
            :href="platform.link"
            @click="
              [SessionStorage.set('platform', platform.text), platform.click()]
            "
          >
            <q-icon :name="`img:/icons/${platform.text}.svg`" size="40px" />
          </q-btn>

          <div id="naverIdLogin" style="display: none"></div>
        </div>
      </q-card-section>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { SessionStorage } from 'quasar';
import { onBeforeMount, onMounted, ref } from 'vue';

const type = ref('login');
const setFormState = () => {
  return {
    identity: '',
    password: '',
    confirmPassword: '',
    nickname: '',
  };
};
const formState = ref(setFormState());
const passwordOff = ref(false);
const confirmPasswordOff = ref(false);

const loginSubmit = async () => {
  //
};

const easy = [
  {
    text: 'kakao',
    link: `https://kauth.kakao.com/oauth/authorize?client_id=${process.env.KAKAO_REST_KEY}&redirect_uri=${process.env.REDIRECT}&response_type=code`,
    click: () => {
      //
    },
  },
  {
    text: 'naver',
    click: () => {
      document.getElementById('naverIdLogin_loginButton')?.click();
    },
  },
  {
    text: 'google',
    click: () => {
      //
    },
  },
  {
    text: 'apple',
    click: () => {
      //
    },
  },
];

const naver = () => {
  const naverLogin = new window.naver.LoginWithNaverId({
    clientId: process.env.NAVER_CLIENT,
    callbackUrl: process.env.REDIRECT,
    isPopup: false,
    loginButton: {},
    callbackHandle: true,
  });

  naverLogin.init();
};

onMounted(() => {
  setTimeout(() => {
    naver();
  });
});
</script>
