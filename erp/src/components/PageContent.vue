<template>
  <div class="page__main__area">
    <div class="page__area">
      <div class="content_area">
        <v-list-item-title class="text-h5 text-capitalize page_title">
          <b>{{ $t(`nav.${$route.name.toLowerCase()}`) }}</b>
        </v-list-item-title>
        <div class="header_toolbar">
          <div class="d-flex">
            <slot name="toolbar"></slot>
          </div>
        </div>
        <div class="main_content">
          <slot name="main"></slot>
        </div>
      </div>
    </div>
    <diaglog-mobile v-if="isMobileVersion">
      <template v-slot:body>
        <slot name="side"></slot>
      </template>
    </diaglog-mobile>
    <info-area v-else>
      <slot name="side"></slot>
    </info-area>
  </div>
</template>

<script>
import DiaglogMobile from "@/components/diaglogMobile.vue";
import InfoArea from "./InfoArea.vue";

export default {
  name: "PageContent",
  components: {
    DiaglogMobile,
    InfoArea,
  },
  methods: {},
};
</script>
