<template>
  <q-fab
    v-model="fabState"
    vertical-actions-align="right"
    direction="down"
    padding="14px"
    unelevated
    square
  >
    <template v-slot:icon>
      <q-icon name="settings" size="24px" class="absolute-center" />
    </template>

    <template v-slot:active-icon>
      <q-icon name="settings" size="24px" />
    </template>

    <div class="fab-menu-container z-top spacing-y-sm">
      <q-fab-action
        :label="$t('word.login')"
        class="full-width"
        no-caps
        unelevated
        @click="loginDialog"
      >
        <template v-slot:icon>
          <q-icon name="login" size="30px" />
        </template>
      </q-fab-action>

      <div class="full-width px-8 row items-center justify-between">
        <q-icon name="nightlight_round" size="30px" />
        <ThemeSwitch />
      </div>

      <div class="full-width px-8 row items-center justify-between no-wrap">
        <q-icon name="language" size="30px" />
        <TranslateLanguage />
      </div>

      <q-fab-action
        :label="$t('word.reset')"
        class="full-width"
        no-caps
        unelevated
      >
        <template v-slot:icon>
          <q-icon name="cached" size="30px" />
        </template>
      </q-fab-action>
    </div>
  </q-fab>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useQuasar } from 'quasar';
import ThemeSwitch from 'src/components/ThemeSwitch.vue';
import TranslateLanguage from 'src/components/TranslateLanguage.vue';
import LoginDialog from 'src/components/dialog/LoginDialog.vue';

const $q = useQuasar();
const fabState = ref(false);

const loginDialog = () => {
  $q.dialog({ component: LoginDialog });
};
</script>

<style lang="scss">
.fab-menu-container {
  background: $light-02;
  box-shadow: 0px 4px 4px rgba(255, 255, 255, 0.3),
    inset 0px 1px 0px rgba(149, 149, 149, 0.25);
  backdrop-filter: blur(6px);
  border-radius: 6px;
  padding: 5px 7px;
  min-width: 180px;

  .q-btn {
    align-items: baseline;
    margin-top: 2.5px;
    margin-bottom: 2.5px;

    .q-btn__content {
      display: flex;
      width: 100%;
      justify-content: space-between;
    }

    .q-fab__label--internal-right {
      padding: 0;
    }

    .q-fab__label {
      font-size: 16px;
      line-height: 19px;
    }
  }
}

.body--dark {
  .fab-menu-container {
    background: rgba(33, 36, 43, 0.6);
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.3),
      inset 0px 1px 0px rgba(149, 149, 149, 0.25);
  }
}
</style>
