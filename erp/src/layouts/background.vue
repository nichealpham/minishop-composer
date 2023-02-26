<template>
  <backdrop-blur>
    <router-view />
  </backdrop-blur>
</template>

<script>
import BackdropBlur from "@/components/BackdropBlur.vue";

export default {
  components: { BackdropBlur },
  data: function() {
    return {};
  },
  created() {},
  methods: {},
};
</script>
